﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis.Options;

namespace Microsoft.CodeAnalysis.Internal.Log
{
    internal static class FunctionIdOptions
    {
        private static readonly ConcurrentDictionary<FunctionId, Option<bool>> s_options =
            new ConcurrentDictionary<FunctionId, Option<bool>>();

        private static readonly Func<FunctionId, Option<bool>> s_optionCreator = CreateOption;

        private static Option<bool> CreateOption(FunctionId id)
        {
            var name = Enum.GetName(typeof(FunctionId), id);

            return new Option<bool>(nameof(FunctionIdOptions), name, defaultValue: false,
                storageLocations: new LocalUserProfileStorageLocation(@"Roslyn\Internal\Performance\FunctionId\" + name));
        }

        public static Option<bool> GetOption(FunctionId id)
        {
            return s_options.GetOrAdd(id, s_optionCreator);
        }
    }
}