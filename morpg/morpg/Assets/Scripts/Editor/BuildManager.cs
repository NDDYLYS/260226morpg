using UnityEngine;
using UnityEditor;

using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

using System.Diagnostics;
using System.Text;

using UnityEditor.Build.Reporting;

public class BuildManager
{
    static string[] SCENES = FindEnabledEditorScenes();

    //------------------------- [ Common ] ---------------------------
    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    [MenuItem("BuildManager/APK Build/Android/Mono2x")]
    static void AndroidBuild_Mono2x()
    {
        PerformAndroidBuildClient(ScriptingImplementation.Mono2x);
    }

    [MenuItem("BuildManager/APK Build/Android/IL2CPP")]
    static void AndroidBuild_IL2CPP()
    {
        PerformAndroidBuildClient(ScriptingImplementation.IL2CPP);
    }


    static void PerformAndroidBuildClient(ScriptingImplementation _buildType)
    {
        ClearConsole();
        LocalManager localManager = Resources.Load<LocalManager>("Prefabs/LocalManager");

        DateTime startTime = DateTime.Now;
        UnityEngine.Debug.Log(string.Format("<color=cyan>Android Build Start.</color> <color=yellow>StartTime : {0}</color>", startTime));

        string rootFolder = "F:/Build/Roguelike";

        string day = string.Format("{0}/Android/{1:yyMMdd}/{2:HHmmss}", rootFolder, DateTime.Now, DateTime.Now);
        Util.CreateFolder(day);

        PlayerSettings.applicationIdentifier = localManager.AppPackageName;
        //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, string.Format("{0}", _server.ToString().ToUpper()));
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, _buildType);

        PlayerSettings.bundleVersion = localManager.BundleVersion;
        PlayerSettings.Android.bundleVersionCode = localManager.BundleVersionCode;
        
        string folder = string.Format("{0}/{1}_{2}.{3}({4})", day, localManager.AppPackageName, _buildType, localManager.BundleVersion, localManager.BundleVersionCode);
        Util.CreateFolder(folder);

        PlayerSettings.Android.keystoreName = string.Format("F:/NDDY_Keystore.keystore");
        PlayerSettings.Android.keystorePass = "890919";
        PlayerSettings.Android.keyaliasName = "roguelike111111";
        PlayerSettings.Android.keyaliasPass = "111111";

        string buildTargetPath = string.Format("{0}/{1}", folder, "Roguelike.apk");
        UnityEngine.Debug.Log(string.Format("Build to '{0}'", buildTargetPath));

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        BuildReport res = BuildPipeline.BuildPlayer(SCENES, buildTargetPath, BuildTarget.Android, BuildOptions.None);
        if (res.summary.result == BuildResult.Succeeded)
        {
            Process.Start(folder);

            DateTime endTime = DateTime.Now;
            UnityEngine.Debug.Log(string.Format("<color=magenta>Android Build Success.</color> <color=yellow>EndTime : {0}</color>", endTime));
        }
        else
        {
            DateTime endTime = DateTime.Now;
            UnityEngine.Debug.Log(string.Format("<color=red>Android Build Fail.</color> <color=yellow>EndTime : {0}</color> <color=blue>Error.Count : {1}</color> <color=red>Warning.Count : {2}</color>", endTime, res.summary.totalErrors, res.summary.totalWarnings));
            throw new Exception("BuildPlayer failure: " + res);
        }
    }

    [MenuItem("BuildManager/Bundle Build/Android/None")]
    static void AndroidNone()
    {
        BuildBundle(BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("BuildManager/Bundle Build/iOS/None")]
    static void iOSNone()
    {
        // BuildBundle(BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    static void BuildBundle(BuildAssetBundleOptions _option, BuildTarget _target)
    {
        ClearConsole();
        DateTime startTime = DateTime.Now;
        UnityEngine.Debug.Log(string.Format("<color=cyan>{0} Bundle({1}) Build Start.</color> <color=yellow>StartTime : {2}</color>", _target, _option, startTime));

        string path = string.Format("{0}", Path.GetFullPath("AssetBundles/"));
        Util.CreateFolder(path);

        string platformPath = string.Format("{0}/{1}", path, _target);
        Util.CreateFolder(platformPath);

        // 빌드할 번들의 버젼 정보를 가져올 경로
        string bundleVersionPath = string.Format("{0}/BundleInfo.json", platformPath);

        AssetBundlesVersion lastBuildBundle = LoadVersion(bundleVersionPath); // 마지막 빌드
        AssetBundlesVersion newBuildBundle = new AssetBundlesVersion(); // 최신 빌드
        newBuildBundle.AssetVersion = lastBuildBundle.AssetVersion + 1; // 번들을 빌드할 때마다 버젼을 1씩 증가시킨다
        newBuildBundle.AssetBundleFileList = new List<AssetBundlesFileInfo>();

        string versionPath = string.Format("{0}/{1}", platformPath, newBuildBundle.AssetVersion);
        Util.CreateFolder(versionPath);

        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(versionPath, _option, _target);
        string[] getAllAssetBundles = manifest.GetAllAssetBundles();

        foreach (var item in getAllAssetBundles)
        {
            uint crc = 0;
            string filePath = Path.GetFullPath(string.Format("{0}/{1}", versionPath, item));
            Hash128 hash = manifest.GetAssetBundleHash(item);
            BuildPipeline.GetCRCForAssetBundle(filePath, out crc);

            FileInfo leftFileInfo = new FileInfo(filePath);
            AssetBundlesFileInfo info = new AssetBundlesFileInfo()
            {
                AssetName = item,
                FilePath = filePath,

                Hash = hash.ToString(),
                FileSize = (int)leftFileInfo.Length, // byte
                CRC = crc
            };

            newBuildBundle.AssetBundleFileList.Add(info);
        }

        // 이번에 빌드된 번들의 none, add, update, delete를 구분하기 위한 작업
        string bundlePatchInfo = string.Empty;
        List<string> buildAssetNameList = new List<string>();
        if (lastBuildBundle.AssetBundleFileList != null)
        {
            for (int i = 0; i < lastBuildBundle.AssetBundleFileList.Count; i++)
            {
                if (!buildAssetNameList.Contains(lastBuildBundle.AssetBundleFileList[i].AssetName))
                    buildAssetNameList.Add(lastBuildBundle.AssetBundleFileList[i].AssetName);
            }
        }
        if (newBuildBundle.AssetBundleFileList != null)
        {
            for (int i = 0; i < newBuildBundle.AssetBundleFileList.Count; i++)
            {
                if (!buildAssetNameList.Contains(newBuildBundle.AssetBundleFileList[i].AssetName))
                    buildAssetNameList.Add(newBuildBundle.AssetBundleFileList[i].AssetName);
            }
        }

        for (int i = 0; i < buildAssetNameList.Count; i++)
        {
            AssetBundlesFileInfo lastBuild = null;
            AssetBundlesFileInfo newBuild = null;

            if (lastBuildBundle.AssetBundleFileList != null)
                lastBuild = lastBuildBundle.AssetBundleFileList.Find(x => x.AssetName == buildAssetNameList[i]);
            if (newBuildBundle.AssetBundleFileList != null)
                newBuild = newBuildBundle.AssetBundleFileList.Find(x => x.AssetName == buildAssetNameList[i]);

            if (lastBuild != null &&
                newBuild != null)
            {
                // 구빌드와 신빌드 모두 있다면 변경이 되었나 되지 않얐냐

                if (lastBuild.Hash != newBuild.Hash ||
                    lastBuild.CRC != newBuild.CRC)
                {
                    bundlePatchInfo += string.Format("<update>\t{0}\n\n", buildAssetNameList[i]);
                }
                else
                {
                    bundlePatchInfo += string.Format("< none >\t{0}\n\n", buildAssetNameList[i]);
                }
            }
            else if (   lastBuild == null &&
                        newBuild != null)
            {
                // 구빌드는 없고 신빌드는 있다면 추가

                bundlePatchInfo += string.Format("< add  >\t{0}\n\n", buildAssetNameList[i]);
            }
            else if (   lastBuild != null &&
                        newBuild == null)
            {
                // 구빌드는 있었는데 신빌드는 없다면 삭제

                bundlePatchInfo += string.Format("<delete>\t{0}\n\n", buildAssetNameList[i]);
            }
        }

        SaveFileBundlePatchInfo(newBuildBundle.AssetVersion.ToString(), _target, bundlePatchInfo);
        // 이번에 빌드된 번들의 none, add, update, delete를 구분하기 위한 작업

        SaveVersion(bundleVersionPath, newBuildBundle); // 빌드된 번들의 정보를 저장한다
        Process.Start(string.Format("{0}/{1}/BundlePatchInfo", path, _target));

        DateTime endTime = DateTime.Now;
        UnityEngine.Debug.Log(string.Format("<color=magenta>{0} Bundle({1}) Build End.</color> <color=yellow>EndTime : {2}</color>", _target, _option, endTime));
    }

    static void SaveFileBundlePatchInfo(string _fileName, BuildTarget _target, string _text)
    {
        UnityEngine.Debug.Log(string.Format("<color=yellow>SaveFileBundlePatchInfo : {0}/{1} Build</color>", _target, _fileName));

        string path = string.Format("{0}", Path.GetFullPath(string.Format("AssetBundles/{0}/BundlePatchInfo", _target)));
        Util.CreateFolder(path);

        string fullPath = string.Format("{0}/{1}.txt", path, _fileName);
        
        File.WriteAllText(fullPath, _text, Encoding.UTF8);
    }

    [MenuItem("BuildManager/Bundle Upload/Android")]
    static void BundleUpload_Android()
    {
        BundleFirebaseStorage_Upload(BuildTarget.Android);
    }

    [MenuItem("BuildManager/Bundle Upload/iOS")]
    static void BundleUpload_iOS()
    {
        BundleFirebaseStorage_Upload(BuildTarget.iOS);
    }


    static void BundleFirebaseStorage_Upload(BuildTarget _target)
    {
        ClearConsole();
        DateTime startTime = DateTime.Now;
        UnityEngine.Debug.Log(string.Format("<color=cyan>{0} Bundle Upload Start.</color> <color=yellow>StartTime : {1}</color>", _target, startTime));

        //FirebaseManager.Instance.Init();

        string bundleVersionPath = string.Format("{0}", Path.GetFullPath(string.Format("AssetBundles/{0}/BundleInfo.json", _target)));
        AssetBundlesVersion bundleVersion = LoadVersion(bundleVersionPath);
        int version = bundleVersion.AssetVersion;

        //FirebaseManager.Instance.FirebaseStorage_Upload(bundleVersionPath, _target.ToString(), -1, "BundleInfo.json", false);

        for (int i = 0; i < bundleVersion.AssetBundleFileList.Count; i++)
        {
            bool isLast = (bundleVersion.AssetBundleFileList.Count - 1 <= i) ? true : false;
            //FirebaseManager.Instance.FirebaseStorage_Upload(bundleVersion.AssetBundleFileList[i].FilePath, _target.ToString(), version, bundleVersion.AssetBundleFileList[i].AssetName, isLast);
        }

        DateTime endTime = DateTime.Now;
        UnityEngine.Debug.Log(string.Format("<color=magenta>{0} Build Upload End.</color> <color=yellow>EndTime : {1}</color>", _target, endTime));
    }
    
    public static void SaveVersion(string _path, AssetBundlesVersion _version)
    {
        string serialized = Util.JsonToClass<AssetBundlesVersion>(_version);

        using (StreamWriter outputFile = new StreamWriter(_path, false))
        {
            outputFile.WriteLine(serialized);
            outputFile.Close();
        }
    }

    public static AssetBundlesVersion LoadVersion(string _path)
    {
        AssetBundlesVersion version = new AssetBundlesVersion();

        if (File.Exists(_path))
        {
            StreamReader ReadFile = new StreamReader(_path);
            string data = ReadFile.ReadLine();
            ReadFile.Close();
            version = Util.ClassToJson<AssetBundlesVersion>(data);
        }
        else
        {
            version.AssetVersion = 0;
        }
        return version;
    }

    public static void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
}