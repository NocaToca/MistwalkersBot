using System;

public class Status{
    public static Sleep Sleep {get{return new Sleep();}}
    public static Burn Burn {get{return new Burn();}}
}

public class Sleep : Status{
    public override string ToString(){
        return "Sleep";
    }
}

public class Burn : Status{
    public override string ToString()
    {
        return "Burn";
    }
}