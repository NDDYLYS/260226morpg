using System.Collections.Generic;

[System.Serializable]
public class AssetBundlesVersion
{
    public int AssetVersion { get; set; }
    public List<AssetBundlesFileInfo> AssetBundleFileList { get; set; }
}