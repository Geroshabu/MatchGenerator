using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("MatchGeneratorTest")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MatchGeneratorTest")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照できなくなります。このアセンブリ内で COM から型にアクセスする必要がある場合は、 
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("bee1e02b-2c59-4a87-9276-dad9b7ebb511")]

// アセンブリのバージョン情報は次の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      Revision
//
// すべての値を指定するか、以下のように '*' を使用してビルド番号とリビジョン番号を 
// 既定値にすることができます:
//[アセンブリ: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.*")]

// 静的フィールドを複数のテストで扱うため, テストの並列実行はしない
[assembly: CollectionBehavior(DisableTestParallelization = true)]
