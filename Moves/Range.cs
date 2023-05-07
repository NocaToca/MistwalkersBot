

namespace Moves{

    public class Range{

        public enum Type{Melee,Single,Room}; //This class will do more later just putting this here for now

        public Type ranged_type;
        public int tile_range;

        public override string ToString()
        {
            return ranged_type.ToString() + " | Tile Range: " + tile_range.ToString();
        }

    }

}