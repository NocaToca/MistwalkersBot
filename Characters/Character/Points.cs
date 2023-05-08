
using System.Runtime.InteropServices;

namespace Characters{

    public partial class Character{

/****************************************************************************POINTS**********************************************************************************************/

        public enum PointType{
            PowerPoints,
            BellyPoints,
            HealthPoints
        }
        public unsafe class Points{
            public float max_power_points {get; private set;}
            public float* current_power_points;

            public float max_belly_points {get; private set;}
            public float* current_belly_points;

            public float max_health_points {get; private set;}
            public float* current_health_points;

            public unsafe Points(float hp, float bp, float pp){
                max_belly_points = bp;
                current_belly_points = (float*) Marshal.AllocHGlobal(sizeof(float));
                *current_belly_points = bp;

                max_health_points = hp;
                current_health_points = (float*) Marshal.AllocHGlobal(sizeof(float));
                *current_health_points = hp;

                max_power_points = pp;
                current_power_points = (float*) Marshal.AllocHGlobal(sizeof(float));
                *current_power_points = pp;
            }

            ~Points() {
                Marshal.FreeHGlobal((IntPtr)current_belly_points);
                Marshal.FreeHGlobal((IntPtr)current_health_points);
                Marshal.FreeHGlobal((IntPtr)current_power_points);
            }

            public int Restore(PointType type, float amount, bool percentage){

                float max = 0.0f;
                if(type == PointType.BellyPoints){
                    max = max_belly_points;
                } else
                if(type == PointType.PowerPoints){
                    max = max_power_points;
                } else {
                    max = max_health_points;
                }

                float* current = GrabCurrent(type);

                if(percentage){
                    float value = *current + (max * amount);
                    *current = (value >= max) ? max : value;
                } else {
                    *current = (*current + amount >= max) ? max : *current + amount;
                }

                return (int)(amount - (max - *current));
            }

            public float* GrabCurrent(PointType type){
                if(type == PointType.BellyPoints){
                    return current_belly_points;
                } else
                if(type == PointType.PowerPoints){
                    return current_power_points;
                } else {
                    return current_health_points;
                }
            }

        }

        public Points main_points;

        private void InitPoints(){
            main_points = new Points(10.0f, 10.0f, 10.0f);
        }

        private void InitPoints(PointFields point_fields){
            main_points = new Points(point_fields.health_points, point_fields.belly_points, point_fields.power_points);
        }
/*********************************************************************************************************************************************************************************/

    }

}