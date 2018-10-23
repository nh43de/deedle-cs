replace

"
FSharpFunc
OptionalValue<([a-z]+)>
FSharpList<([a-z]+)>
\\u002[0-9]{1,2}
\\u002D[0-9]+
\\u002[a-f]
\\u002[a-f]
\\u003[a-f]
\\u004[0-9]{1,5}
\\u007[a-f]
\[CompilerGenerated]
\[DebuggerNonUserCode]
\[DebuggerBrowsable.*
\[CompilationMapping]
"

"
Func
$1
IEnumerable<$1>












"

replace

\r\n\r\n

\r\n  ///</summary> \r\n\r\n ///<summary> \r\n