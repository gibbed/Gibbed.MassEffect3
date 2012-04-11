/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System.Collections.Generic;

namespace Gibbed.MassEffect3.SaveEdit
{
    internal class PlayerClass
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public PlayerClass(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        public static List<PlayerClass> GetClasses()
        {
            var classes = new List<PlayerClass>
            {
                new PlayerClass("SFXGame.SFXPawn_PlayerAdept", Properties.Localization.PlayerClass_Adept),
                new PlayerClass("SFXGame.SFXPawn_PlayerEngineer", Properties.Localization.PlayerClass_Engineer),
                new PlayerClass("SFXGame.SFXPawn_PlayerInfiltrator", Properties.Localization.PlayerClass_Infiltrator),
                new PlayerClass("SFXGame.SFXPawn_PlayerSentinel", Properties.Localization.PlayerClass_Sentinel),
                new PlayerClass("SFXGame.SFXPawn_PlayerSoldier", Properties.Localization.PlayerClass_Soldier),
                new PlayerClass("SFXGame.SFXPawn_PlayerVanguard", Properties.Localization.PlayerClass_Vanguard),
                new PlayerClass("SFXGame.SFXPawn_PlayerAdeptNonCombat", Properties.Localization.PlayerClass_AdeptNonCombat),
                new PlayerClass("SFXGame.SFXPawn_PlayerEngineerNonCombat", Properties.Localization.PlayerClass_EngineerNonCombat),
                new PlayerClass("SFXGame.SFXPawn_PlayerInfiltratorNonCombat", Properties.Localization.PlayerClass_InfiltratorNonCombat),
                new PlayerClass("SFXGame.SFXPawn_PlayerSentinelNonCombat", Properties.Localization.PlayerClass_SentinelNonCombat),
                new PlayerClass("SFXGame.SFXPawn_PlayerSoldierNonCombat", Properties.Localization.PlayerClass_SoldierNonCombat),
                new PlayerClass("SFXGame.SFXPawn_PlayerVanguardNonCombat", Properties.Localization.PlayerClass_VanguardNonCombat),
            };
            return classes;
        }
    }
}
