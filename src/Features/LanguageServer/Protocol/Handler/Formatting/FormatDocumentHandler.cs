﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Host.Mef;
using LSP = Microsoft.VisualStudio.LanguageServer.Protocol;

namespace Microsoft.CodeAnalysis.LanguageServer.Handler
{
    [Shared]
    [ExportLspMethod(LSP.Methods.TextDocumentFormattingName)]
    internal class FormatDocumentHandler : FormatDocumentHandlerBase, IRequestHandler<LSP.DocumentFormattingParams, LSP.TextEdit[]>
    {
        [ImportingConstructor]
        [Obsolete(MefConstruction.ImportingConstructorMessage, error: true)]
        public FormatDocumentHandler()
        {
        }

        public async Task<LSP.TextEdit[]> HandleRequestAsync(Solution solution, LSP.DocumentFormattingParams request, LSP.ClientCapabilities clientCapabilities,
            string? clientName, CancellationToken cancellationToken)
        {
            return await GetTextEditsAsync(solution, request.TextDocument, clientName, cancellationToken).ConfigureAwait(false);
        }
    }
}
