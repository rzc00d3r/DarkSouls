# 与 DarkSouls 模组的集成

这里介绍了其他模组的开发者如何通过 `Mod.Call` 使用模组的 API，为他们的武器添加角色属性需求 (ReqParam) 和伤害补正 (ParamBonus)。

## 方法：`RegisterWeapon`

此方法允许您将武器注册到 DarkSouls 模组的全局补正表中。强烈建议在您的模组重写的 `PostSetupContent()` 方法中调用此方法。

### 调用参数

该方法接受刚好 11 个参数，且顺序严格固定：

| 索引 | 类型 | 描述 | 示例 |
| :--- | :--- | :--- | :--- |
| 0 | `string` | 调用的方法名称。始终为 `"RegisterWeapon"`。 | `"RegisterWeapon"` |
| 1 | `int` | 您物品的内部 ID。 | `ModContent.ItemType<MySword>()` |
| 2 | `int` | 力量需求 (Strength)。 | `20` |
| 3 | `int` | 敏捷需求 (Dexterity)。 | `10` |
| 4 | `int` | 智力需求 (Intelligence)。 | `0` |
| 5 | `int` | 信仰需求 (Faith)。 | `0` |
| 6 | `int` | 力量补正等级。* | `3` (B 级) |
| 7 | `int` | 敏捷补正等级。* | `5` (D 级) |
| 8 | `int` | 智力补正等级。* | `0` (无) |
| 9 | `int` | 信仰补正等级。* | `0` (无) |
| 10 | `float` | 补正饱和度 (Saturation)。默认值：`100f`。 | `100f` |

**关于参数 6-9（补正等级）的注意事项：** 由于第三方模组无法直接访问内部的 `enum ScalingGrade`，因此补正等级以整数 (`int`) 形式传递。请使用下表进行转换：
* `0` = 无补正 (-)
* `1` = S
* `2` = A
* `3` = B
* `4` = C
* `5` = D
* `6` = E

**关于补正饱和度 (Saturation) 的说明：** 这是一个直接增加角色属性加成的乘数。`100f` 是不改变额外伤害公式行为的基础值 (100%)。例如，`150f` 会将额外伤害增加 50%，而 `75f` 则会将伤害降低 25%。

### 使用示例

下面是一个现成的示例，展示了如何为您的模组添加对 DarkSouls 模组的支持。

```csharp
using Terraria.ModLoader;

namespace MyAwesomeMod
{
    public class MyAwesomeMod : Mod
    {
        public override void PostSetupContent()
        {
            // 尝试获取 DarkSouls 模组的实例
            if (ModLoader.TryGetMod("DarkSouls", out Mod darkSoulsMod))
            {
                // 为自定义剑 (MyCustomSword) 注册参数
                // 需求：20 力量，10 敏捷
                // 补正：力量 = B (3)，敏捷 = D (5)
                
                object result = darkSoulsMod.Call(
                    "RegisterWeapon",
                    ModContent.ItemType<Items.MyCustomSword>(), // 1. 物品 ID
                    20, 10, 0, 0,                               // 2-5. 需求 (Str, Dex, Int, Fth)
                    3, 5, 0, 0,                                 // 6-9. 补正等级 (3=B, 5=D, 0=None)
                    100f                                        // 10. 饱和度 (Saturation)
                );

                // 如果集成成功，调用将返回 true。
                // 如果发生错误，将返回 ArgumentException，您可以将其文本输出到日志中。
                if (result is bool success && success)
                {
                    Logger.Info("成功为 MyCustomSword 添加了 DarkSouls 补正！");
                }
                else if (result is System.ArgumentException ex)
                {
                    Logger.Error($"在 DarkSouls 中调用 RegisterWeapon 时出错: {ex.Message}");
                }
            }
        }
    }
}