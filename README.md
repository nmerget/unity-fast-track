# Unity Fast Track

Unity Fast Rack is a template project for games. 🎮

It provides a good starting point and some explanations how to manage some basics.

Feel free to clone the repo and test it with Unity.

---

## Core Concepts

### Singletons

-> TODO

### EventHandler

You can find the EventHandler under  ``Scripts/Utils/EventHandler.cs``.
Purpose of this is to bundle all possible events for the game in one file.

Why should I want to do this?

-> TODO

---

## Dependencies

1. [TextMesh Pro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html): Obvious pick for better text ui 📃
2. [Localization](https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/index.html): Unity provides this
   🏳‍🌈
3. [AsyncAwaitUtil](https://assetstore.unity.com/packages/tools/integration/async-await-support-101056): [Coroutines](https://docs.unity3d.com/Manual/Coroutines.html)
   sucks🤮, async await is easy 🤯
4. [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595): Simple tweening library for ui
   animations ⬆🏃‍♂️

---

## Structure

### Editor

Some scripts which should run in the unity editor in the background:

- DependencyCheck: Will look for all required dependencies
- EventHandlerGenerator: Will generate a controller to trigger events from game objects

### Localization

All assets and strings which should be translated.

### Plugins

All Dependencies should be moved into this to separate them from your code.

### Prefabs

Predefined game objects to bundle or reuse.

### Resources

All files like materials, images, music etc.

### Scenes

All your scenes for your game.

### Scripts

All code you write.