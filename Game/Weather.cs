
public abstract class Weather{
    public static Hail Hail {get{return new Hail();}}
    public static Rain Rain {get{return new Rain();}}
    public static Sunny Sunny {get{return new Sunny();}}
    public static SandStorm SandStorm {get{return new SandStorm();}}
    public static Clear Clear {get{return new Clear();}}
}

public class Hail : Weather{

    public override string ToString()
    {
        return "Hail";
    }

}

public class Rain : Weather{
    public override string ToString()
    {
        return "Rain";
    }
}

public class Sunny : Weather{
    public override string ToString()
    {
        return "Sunny";
    }
}

public class SandStorm : Weather{
    public override string ToString()
    {
        return "Sand Storm";
    }
}

public class Clear : Weather{
    public override string ToString()
    {
        return "Clear";
    }
}