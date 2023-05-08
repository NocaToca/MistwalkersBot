//Our main list of abilities
using System;

namespace Abilities{

    public class Ability{

        public enum Type{Universal, Typal, Individual}

        public Type type;
        public string name;

        public Ability(Type type){
            this.type = type;
        }

    }

}