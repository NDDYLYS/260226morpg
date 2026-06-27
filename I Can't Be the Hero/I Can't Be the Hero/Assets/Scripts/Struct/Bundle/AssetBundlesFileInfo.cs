[System.Serializable]
public class AssetBundlesFileInfo
{
    public string AssetName { get; set; }
    public string FilePath { get; set; }

    public string Hash { get; set; }
    public int FileSize { get; set; }
    public uint CRC { get; set; }
}