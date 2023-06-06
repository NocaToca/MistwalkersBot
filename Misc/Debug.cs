

public static class Debug{

    public static void WriteToDebugFile(string s){
        using (StreamWriter writer = new StreamWriter(@"D:\Discord\MistwalkersBot\debug.txt", true)){
            writer.WriteLine(s);
        }
    }

}