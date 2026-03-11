using System;
using System.Collections.Generic;
using System.IO;


namespace SID.Standard.Control
{
    [Serializable]
    public class Project
    {
        string name;
        string description;
        string path;
        DateTime created;
        DateTime modified;
        List<Connection> connections;

        public Project(string name, string description, string path, DateTime created, DateTime modified)
        {
            this.name = name;
            this.description = description;
            this.path = path;
            this.created = created;
            this.modified = modified;

            connections = new List<Connection>();
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Path { get => path; set => path = value; }
        public DateTime Created { get => created; }
        public DateTime Modified { get => modified; set => modified = value; }
        public List<Connection> Connections { get => connections; set => connections = value; }

        public void Save()
        {
            using (Stream stream = File.Open(Path, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, this);
                stream.Close();
            }
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path) + @"/sid.files");
            
        }
    }

    public class Event_ProjectUpdate : System.EventArgs
    {
        public Project Project { get; set; }
    }
}
