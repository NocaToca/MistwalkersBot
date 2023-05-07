using System;
using System.IO;

namespace FileHandling{

    public class FileWriter : IDisposable{

        private StreamWriter writer;

        public FileWriter(string filepath){
            // Open the file in write mode and create it if it doesn't exist
            writer = new StreamWriter(filepath, false);
        }

        public void WriteLine(string s){
            // Write the string to the file and flush the buffer
            writer.WriteLine(s);
            writer.Flush();
        }

        public void Dispose(){
            // Close the file stream when the object is disposed
            writer.Dispose();
        }

    }

}