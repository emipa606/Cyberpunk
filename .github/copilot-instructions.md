# GitHub Copilot Instructions for Cyberpunk (Continued) Mod

## Mod Overview and Purpose
The Cyberpunk (Continued) mod is an update to the Darques mod, inspired by Itachi x E28's updated version. The mod adds elements of the Cyberpunk 2020 RPG game to RimWorld. It introduces new guns, melee weapons, armors, helmets, and apparel, along with cybernetic implants. The mod removes all vanilla weapons and apparel, and includes options to customize gameplay features like weapon reliability.

## Key Features and Systems
- **New Weapons and Apparel**: The mod introduces a variety of new guns, melee weapons, and apparel items.
- **Cybernetic Implants**: Players can equip cybernetic implants that provide various benefits and drawbacks.
- **Weapon Reliability**: An option to toggle weapon reliability, affecting the chance of weapons jamming.
- **Pardeike's Harmony Library**: Utilizes Harmony for patching and modifying vanilla game mechanics.

## Coding Patterns and Conventions
- **C# Files**: The mod's C# files are located in the `Source/Cyberpunk` directory. Key files include:
  - `CyberpunkMod.cs`: Contains the main mod class and settings handling.
  - `CyberpunkSettings.cs`: Manages mod settings and exposes data.
  - `HediffComp_ImplantHeal.cs` and `HediffComp_MuscleGraft.cs`: Handle specific hediff behaviors for implants and muscle grafts.
- **XML Integration**: XML files are located in the `Defs` directory, such as `HediffDefs`, `ThingCategoryDefs`, and `ThingDefs`. These files define various game elements like hediffs, thing categories, and thing definitions.
- **Harmony Patching**: The mod uses Harmony to patch vanilla game mechanics. For example, `HediffComp_ImplantHeal.cs` and `HediffComp_MuscleGraft.cs` contain Harmony patches for specific behaviors.

## XML Integration
- **Structure**: XML files are organized into directories like `HediffDefs`, `ThingCategoryDefs`, and `ThingDefs`.
- **Definitions**: Each XML file contains multiple `<Def>` elements, defining specific game elements.
- **Example**: `HediffDefs/Hediffs_Local_Bioware_CP.xml` contains definitions for bioware hediffs, including melee damage definitions.

## Harmony Patching
- **Purpose**: Harmony is used to modify vanilla game mechanics without altering the original code.
- **Implementation**: Patches are implemented in C# files. For example, `HediffComp_ImplantHeal.cs` contains methods like `CompExposeData` and `CompPostTick` that are patched using Harmony.
- **Example**: The `HediffComp_ImplantHeal` class patches the behavior of implant healing effects.

## Suggestions for Copilot
- **C# Files**: When working with C# files, Copilot can help with:
  - Generating new mod settings classes.
  - Implementing Harmony patches.
  - Creating new hediff components.
- **XML Files**: When working with XML files, Copilot can help with:
  - Generating new `<Def>` elements.
  - Modifying existing definitions.
  - Ensuring XML structure and syntax are correct.
- **General Tips**:
  - Use Copilot to quickly scaffold new code and XML structures.
  - Review generated code to ensure it aligns with the mod's coding conventions and gameplay mechanics.
  - Always test changes in a controlled environment to avoid introducing bugs.

By following these guidelines, you can effectively use GitHub Copilot to enhance your work on the Cyberpunk (Continued) mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

