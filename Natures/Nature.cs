using Characters;

namespace Natures{

    public enum NatureType{
        Brave,
        Naughty,
        Impish,
        Quirky,
        Jolly,
        Adamant,
        Serious,
        Bashful,
        Lonely,
        Hasty,
        Rash,
        Relaxed,
        Hardy,
        Mild,
        Gentle,
        Naive,
        Modest,
        Docile,
        Lax,
        Sassy,
        Quiet,
        Calm,
        Bold,
        Timid
    }

    //List of enums that does stuff with them
    public static class Nature{
        public static Tuple<AttributeType, AttributeType> GrabNatureAttributes(NatureType type){
            switch(type){
                case NatureType.Brave:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Strength, AttributeType.Wisdom);
                case NatureType.Naughty:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Strength, AttributeType.Intelligence);
                case NatureType.Impish:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Strength, AttributeType.Charisma);
                case NatureType.Quirky:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Strength, AttributeType.Constitution);
                case NatureType.Jolly:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Charisma, AttributeType.Constitution);
                case NatureType.Adamant:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Charisma, AttributeType.Wisdom);
                case NatureType.Serious:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Charisma, AttributeType.Intelligence);
                case NatureType.Bashful:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Charisma, AttributeType.Dexterity);
                case NatureType.Lonely:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Dexterity, AttributeType.Charisma);
                case NatureType.Hasty:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Dexterity, AttributeType.Constitution);
                case NatureType.Rash:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Dexterity, AttributeType.Charisma);
                case NatureType.Relaxed:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Constitution, AttributeType.Dexterity);
                case NatureType.Hardy:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Constitution, AttributeType.Intelligence);
                case NatureType.Mild:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Constitution, AttributeType.Wisdom);
                case NatureType.Gentle:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Constitution, AttributeType.Strength);
                case NatureType.Naive:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Constitution, AttributeType.Charisma);
                case NatureType.Modest:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Intelligence, AttributeType.Strength);
                case NatureType.Docile:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Intelligence, AttributeType.Wisdom);
                case NatureType.Lax:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Intelligence, AttributeType.Dexterity);
                case NatureType.Sassy:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Intelligence, AttributeType.Charisma);
                case NatureType.Quiet:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Wisdom, AttributeType.Constitution);
                case NatureType.Calm:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Wisdom, AttributeType.Strength);
                case NatureType.Bold:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Wisdom, AttributeType.Intelligence);
                case NatureType.Timid:
                    return new Tuple<AttributeType, AttributeType>(AttributeType.Wisdom, AttributeType.Charisma);
                default:
                    throw new ArgumentException("Unknown nature type: " + type);
            }
        }


    }

}