﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BiteRight.Resources.Resources.Common {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Common {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Common() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("BiteRight.Resources.Resources.Common.Common", typeof(Common).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string page_number_not_valid {
            get {
                return ResourceManager.GetString("page_number_not_valid", resourceCulture);
            }
        }
        
        public static string page_size_not_valid {
            get {
                return ResourceManager.GetString("page_size_not_valid", resourceCulture);
            }
        }
        
        public static string pagination_params_null {
            get {
                return ResourceManager.GetString("pagination_params_null", resourceCulture);
            }
        }
        
        public static string unknown_error {
            get {
                return ResourceManager.GetString("unknown_error", resourceCulture);
            }
        }
        
        public static string not_found {
            get {
                return ResourceManager.GetString("not_found", resourceCulture);
            }
        }
        
        public static string internal_error {
            get {
                return ResourceManager.GetString("internal_error", resourceCulture);
            }
        }
        
        public static string validation_error {
            get {
                return ResourceManager.GetString("validation_error", resourceCulture);
            }
        }
        
        public static string query_too_long {
            get {
                return ResourceManager.GetString("query_too_long", resourceCulture);
            }
        }
        
        public static string filtering_params_null {
            get {
                return ResourceManager.GetString("filtering_params_null", resourceCulture);
            }
        }
    }
}
