using MelonLoader;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppInterop.Runtime;
//using Il2CppAssets.Scripts.Simulation.Bloons;
[assembly: MelonInfo(typeof(pierceToDamage.pierceToDamage),"Pierce to damage","1.0.0","Silentstorm")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace pierceToDamage{
    public class pierceToDamage:MelonMod{
        /*private static MelonLogger.Instance mllog=null;
        public override void OnLateInitializeMelon(){
            mllog=LoggerInstance;
        }
        public static void Log(object thingtolog,string type="msg"){
            switch(type){
                case"msg":
                    mllog.Msg(thingtolog);
                    break;
                case"warn":
                    mllog.Warning(thingtolog);
                    break;
                 case"error":
                    mllog.Error(thingtolog);
                    break;
            }
        }*/
        [HarmonyPatch(typeof(Projectile),"Initialise")]
        public class ProjectileInitialise_Patch{
            public static void Prefix(ref Model modelToUse){
                ProjectileModel proj=modelToUse.Cast<ProjectileModel>();
                try{
                    foreach(Model model in proj.behaviors){
                        if(model.GetIl2CppType()==Il2CppType.Of<DamageModel>()){
                            model.Cast<DamageModel>().damage+=proj.pierce-1;
                        }
                        if(model.GetIl2CppType()==Il2CppType.Of<DamageInRingRadiusModel>()){
                            model.Cast<DamageInRingRadiusModel>().damage+=proj.pierce-1;
                        }
                    }
                }catch{}
                proj.pierce=1;
            }
        }
        /*[HarmonyPatch(typeof(Bloon),"Damage")]
        public class BloonDamage_Patch{
            public static void Prefix(Projectile projectile){
                Log("bloon");
                Log(projectile.pierce);
                try{
                    foreach(Model model in projectile.projectileModel.behaviors){
                        if(model.GetIl2CppType()==Il2CppType.Of<DamageModel>()){
                            Log(model.Cast<DamageModel>().damage);
                        }
                        if(model.GetIl2CppType()==Il2CppType.Of<DamageInRingRadiusModel>()){
                            Log(model.Cast<DamageInRingRadiusModel>().damage);
                        }
                    }
                }catch{}
            }
        }*/
    }
}