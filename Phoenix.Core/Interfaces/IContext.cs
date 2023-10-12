using dnlib.DotNet;
using Phoenix.Core.Interfaces;

namespace Phoenix.Core.Interfaces
{
    internal interface IContext
    {
        bool ImportAssembly();

        void ExportAssembly();

        IOptions Options { get; }
        ILogger Logger { get; }
        AssemblyDef assemblyDef { get; set; }
        ModuleDef moduleDef { get; set; }
        ModuleDefMD moduleDefMD { get; set; }
        TypeDef typeDef { get; set; }
        MethodDef cctor { get; set; }
    }
}