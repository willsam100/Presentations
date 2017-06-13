#r "System.ServiceModel"
#r "/Users/Ashley/code/FsReveal-master/packages/FSharp.Data.TypeProviders/lib/net40/FSharp.Data.TypeProviders.dll"
open FSharp.Data.TypeProviders

type Wsdl1 = WsdlService<"http://api.microsofttranslator.com/V2/Soap.svc">

let ctxt = Wsdl1.GetBasicHttpBinding_LanguageService()