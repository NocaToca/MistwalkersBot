using System;

public class Status{
    public static Sleep Sleep {get{return new Sleep();}}
    public static Burn Burn {get{return new Burn();}}
    public static Paralysis Paralysis{get{return new Paralysis();}}
    public static DamageOverTime DoT {get{return new DamageOverTime();}}
    public static Flinch Flinch {get{return new Flinch();}}
    public static Confusion Confusion {get{return new Confusion();}}
    public static Poison Poison {get{return new Poison();}}
    public static Immune Immune {get{return new Immune();}}
    public static Prone Prone {get{return new Prone();}}
    public static Frozen Frozen {get{return new Frozen();}}
    public static Charge Charge {get{return new Charge();}}
    public static Reflect Reflect {get{return new Reflect();}}
}
public class Reflect : Status{
    public override string ToString(){
        return "Reflect";
    }
}
public class Charge : Status{
    public override string ToString(){
        return "Charge";
    }
}
public class Frozen : Status{
    public override string ToString(){
        return "Frozen";
    }
}
public class Prone : Status{
    public override string ToString(){
        return "Prone";
    }
}
public class Poison : Status{
    public override string ToString(){
        return "Poison";
    }
}
public class Immune : Status{
    public override string ToString()
    {
        return "Immune";
    }
}
public class Confusion : Status{
    public override string ToString(){
        return "Confusion";
    }
}
public class Flinch : Status{
    public override string ToString()
    {
        return "Flinch";
    }
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
public class Paralysis : Status{
    public override string ToString()
    {
        return "Paralysis";
    }
}

public class DamageOverTime : Status{
    public float amount;

    public override string ToString()
    {
        return "DoT for " + amount + "% each turn.";
    }

}