# Integration with the DarkSouls Mod

This document describes how developers of other mods can add character stat requirements (ReqParam) and damage scaling (ParamBonus) to their weapons using the mod's API via `Mod.Call`.

## Method: `RegisterWeapon`

This method allows you to register your weapons in the DarkSouls mod's global scaling table. It is strictly recommended to call this method inside your mod's overridden `PostSetupContent()` method.

### Call Arguments

The method accepts exactly 11 arguments in a strictly defined order:

| Index | Type | Description | Example |
| :--- | :--- | :--- | :--- |
| 0 | `string` | The name of the called method. Always `"RegisterWeapon"`. | `"RegisterWeapon"` |
| 1 | `int` | The internal ID of your item. | `ModContent.ItemType<MySword>()` |
| 2 | `int` | Strength requirement. | `20` |
| 3 | `int` | Dexterity requirement. | `10` |
| 4 | `int` | Intelligence requirement. | `0` |
| 5 | `int` | Faith requirement. | `0` |
| 6 | `int` | Strength scaling level.* | `3` (Grade B) |
| 7 | `int` | Dexterity scaling level.* | `5` (Grade D) |
| 8 | `int` | Intelligence scaling level.* | `0` (None) |
| 9 | `int` | Faith scaling level.* | `0` (None) |
| 10 | `float` | Scaling Saturation. Default value: `100f`. | `100f` |

**Attention regarding arguments 6-9 (Scaling Grades):** Since third-party mods do not have direct access to the internal `enum ScalingGrade`, the scaling levels are passed as integers (`int`). Use the following table for conversion:
* `0` = No scaling (-)
* `1` = S
* `2` = A
* `3` = B
* `4` = C
* `5` = D
* `6` = E

**Note on Scaling Saturation:** This is a multiplier that directly increases the bonus from character stats. `100f` is the base value that does not change the behavior of the bonus damage formula (100%). For example, `150f` will increase the bonus damage by 50%, while `75f` will reduce the damage by 25%.

### Usage Example

Below is a ready-to-use example of how to add DarkSouls mod support to your mod.

```csharp
using Terraria.ModLoader;

namespace MyAwesomeMod
{
    public class MyAwesomeMod : Mod
    {
        public override void PostSetupContent()
        {
            // Trying to get the DarkSouls mod instance
            if (ModLoader.TryGetMod("DarkSouls", out Mod darkSoulsMod))
            {
                // Registering parameters for a custom sword (MyCustomSword)
                // Requirements: 20 Strength, 10 Dexterity
                // Scaling: Strength = B (3), Dexterity = D (5)
                
                object result = darkSoulsMod.Call(
                    "RegisterWeapon",
                    ModContent.ItemType<Items.MyCustomSword>(), // 1. Item ID
                    20, 10, 0, 0,                               // 2-5. Requirements (Str, Dex, Int, Fth)
                    3, 5, 0, 0,                                 // 6-9. Scaling (3=B, 5=D, 0=None)
                    100f                                        // 10. Saturation
                );

                // If integration is successful, the call will return true.
                // In case of an error, it will return an ArgumentException, the text of which can be logged.
                if (result is bool success && success)
                {
                    Logger.Info("Successfully added DarkSouls scaling for MyCustomSword!");
                }
                else if (result is System.ArgumentException ex)
                {
                    Logger.Error($"Error when calling RegisterWeapon in DarkSouls: {ex.Message}");
                }
            }
        }
    }
}