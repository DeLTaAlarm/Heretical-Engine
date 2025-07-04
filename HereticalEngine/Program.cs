

using HCore;

AssemblyLoader assemblyLoader = new AssemblyLoader();
using GameLoopCore game = new GameLoopCore(assemblyLoader);
assemblyLoader.LoadAssembly();
game.Run();
