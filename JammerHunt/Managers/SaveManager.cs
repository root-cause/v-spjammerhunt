using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using JammerHunt.Enums;

namespace JammerHunt.Managers
{
    public static class SaveManager
    {
        #region Constants
        private const int SaveVersion = 1;
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SPJammerHunt");
        private static readonly string SaveFile = Path.Combine(SaveDirectory, "save.xml");
        #endregion

        #region Properties
        public static bool[] Progress { get; private set; } = new bool[Constants.MaxJammers];
        #endregion

        #region Methods
        public static JammerHuntStage Load()
        {
            if (!Directory.Exists(SaveDirectory) || !File.Exists(SaveFile))
            {
                Save();
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(SaveFile);

            int saveVersion = Convert.ToInt32(doc.SelectSingleNode("/JammerHuntSave/@version")?.Value);
            if (SaveVersion != saveVersion)
            {
                throw new NotImplementedException("Save version handling is not implemented.");
            }

            XmlNode progressData = doc.SelectSingleNode("//Progress");
            if (progressData == null)
            {
                throw new Exception("Progress not found in save file.");
            }
            else
            {
                Progress = progressData.InnerText.Select(c => c == '1').ToArray();

                if (Progress.Length != Constants.MaxJammers)
                {
                    throw new Exception("Progress does not match jammer data.");
                }

                if (Progress.Count(isDestroyed => isDestroyed) == Constants.MaxJammers)
                {
                    return JammerHuntStage.Complete;
                }
                else
                {
                    return JammerHuntStage.Hunting;
                }
            }
        }

        public static void Save()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("JammerHuntSave");
            root.SetAttribute("version", SaveVersion.ToString());

            XmlNode progressData = doc.CreateElement("Progress");
            StringBuilder sb = new StringBuilder();

            foreach (bool item in Progress)
            {
                sb.Append(item ? 1 : 0);
            }

            progressData.InnerText = sb.ToString();

            root.AppendChild(progressData);
            doc.AppendChild(root);

            Directory.CreateDirectory(SaveDirectory);
            doc.Save(SaveFile);
        }
        #endregion
    }
}
