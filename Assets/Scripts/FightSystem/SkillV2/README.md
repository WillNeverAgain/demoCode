# SkillV2（表驱动技能系统）说明（JSON / Excel 配表）

## 设计目标（你给的约束）
- **效果（Effect）**：技能最小判定单元；任何技能逻辑不允许越过 Effect。
- **行动（Action）**：最小执行单元；Effect 的执行步骤由行为树（BT）节点驱动，Action 为叶子节点。
- **行为树（BT）**：每个 Effect 都包含一棵 BT，支持 `And / Or / Not / If / ForEachTarget / Action`。
- **分阶段执行**：`Skill -> Phase -> Stage(结算阶段) -> Effects -> BT -> Actions`。
- **阶段式结算**：每个 Stage 内先缓冲事件（EventBuffer），Stage 结束统一 Release；Release 后再进入下一 Stage，
  从而保证“上一阶段的结果（如伤害后血量变化）会影响下一阶段的条件/目标筛选”。

## 推荐配表方式
- **Excel 配表（设计师友好）**：用 `Tools/SkillV2TableExporter/SkillV2_Template.xlsx` 按模板填表
- **导出 JSON（运行时加载）**：用 `Tools/SkillV2TableExporter/export_skillv2_excel_to_json.py` 导出 `SkillConfigData.json`
- **运行时只加载 JSON**：Unity 端用 `SkillV2.Serialization.SkillV2Json` 读取 JSON 并执行

> 为什么运行时不直接读 Excel？
> - Unity 运行时解析 xlsx 需要额外第三方库（体积/兼容性/平台限制都比较麻烦）
> - 把 Excel 作为“源数据”，导出成 JSON 作为“运行数据”是最稳妥的生产链路

## 目录结构
- `SkillV2/Configs`：所有表结构（Serializable），核心输出结构：`SkillConfigData`
- `SkillV2/Serialization`：JSON 加载/保存工具（`SkillV2Json`）
- `SkillV2/Runtime`：SkillRuntimeV2 / Pipeline / 工具与默认事件处理器
- `SkillV2/BT`：行为树解释执行
- `SkillV2/Predicates`：Predicate 评估（If/Effect条件/技能准入）
- `SkillV2/Expr`：表达式（伤害/治疗等数值）
- `SkillV2/Targeting`：目标选择（AOE Square / Diamond）
- `SkillV2/Actions`：行动执行器（Damage/Heal/AddBuff/RemoveBuff/Move）

## 运行入口
- `SkillRuntimeV2.Execute(SkillV2ExecuteRequest, SkillV2RuntimeContext)`

### 典型加载示例（StreamingAssets）
```csharp
using MyFrame.FightSystem.SkillV2.Serialization;
using MyFrame.FightSystem.SkillV2.Runtime;

var cfg = SkillV2Json.LoadFromStreamingAssets("SkillV2/skills/skill_fireball.json");
var runtime = new SkillRuntimeV2(cfg, hostUnit);
var report = runtime.Execute(new SkillV2ExecuteRequest(centerUnit), worldCtx);
```
