﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AJ.DuplexService.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AJ.DuplexService.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Condition not met!.
        /// </summary>
        internal static string MsgConditionFailed {
            get {
                return ResourceManager.GetString("MsgConditionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Format failure:\n    Exception={0}\n    Format=\&quot;{1}\&quot;\n    args.Length={2}.
        /// </summary>
        internal static string MsgInvalidFormatString {
            get {
                return ResourceManager.GetString("MsgInvalidFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid switch value &apos;{1}&apos; for &apos;{0}&apos;..
        /// </summary>
        internal static string MsgInvalidSwitchValue {
            get {
                return ResourceManager.GetString("MsgInvalidSwitchValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument ‘{0}’ should not be empty!.
        /// </summary>
        internal static string MsgParamEmpty {
            get {
                return ResourceManager.GetString("MsgParamEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument ‘{0}’ should not be NULL!.
        /// </summary>
        internal static string MsgParamNull {
            get {
                return ResourceManager.GetString("MsgParamNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (no param).
        /// </summary>
        internal static string ValueNoParam {
            get {
                return ResourceManager.GetString("ValueNoParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (null).
        /// </summary>
        internal static string ValueNull {
            get {
                return ResourceManager.GetString("ValueNull", resourceCulture);
            }
        }
    }
}
