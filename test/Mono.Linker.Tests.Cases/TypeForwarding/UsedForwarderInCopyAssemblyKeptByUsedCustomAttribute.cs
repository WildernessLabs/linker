﻿using System.Runtime.CompilerServices;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using Mono.Linker.Tests.Cases.TypeForwarding.Dependencies;

namespace Mono.Linker.Tests.Cases.TypeForwarding
{
	// Actions:
	// copy - This assembly
	// link - Forwarder.dll and Implementation.dll
	[SetupLinkerAction ("copy", "test")]
	[SetupCompileBefore ("Forwarder.dll", new[] { "Dependencies/ReferenceImplementationLibrary.cs" }, defines: new[] { "INCLUDE_REFERENCE_IMPL" })]

	// After compiling the test case we then replace the reference impl with implementation + type forwarder
	[SetupCompileAfter ("Implementation.dll", new[] { "Dependencies/ImplementationLibrary.cs" })]
	[SetupCompileAfter ("Forwarder.dll", new[] { "Dependencies/ForwarderLibrary.cs" }, references: new[] { "Implementation.dll" })]

	[KeptTypeInAssembly ("Forwarder.dll", typeof (ImplementationLibraryAttribute))]
	[KeptTypeInAssembly ("Implementation.dll", typeof (ImplementationLibraryAttribute))]

	[Kept]
	[KeptMember (".ctor()")]
	public class UsedForwarderInCopyAssemblyKeptByUsedCustomAttribute
	{
		[Kept]
		[KeptAttributeAttribute (typeof (ImplementationLibraryAttribute))]
		[ImplementationLibrary]
		public static void Main ()
		{
		}
	}
}