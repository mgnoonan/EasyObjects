using System;

namespace System.Configuration
{
    // Summary:
    //     Provides programmatic access to the connection strings configuration-file
    //     section.
    public sealed class DynamicQueryProviderSection : ConfigurationSection
    {
        // Summary:
        //     Initializes a new instance of the System.Configuration.ConnectionStringsSection
        //     class.
        public DynamicQueryProviderSection();

        // Summary:
        //     Gets a System.Configuration.ConnectionStringSettingsCollection collection
        //     of System.Configuration.ConnectionStringSettings objects.
        //
        // Returns:
        //     A System.Configuration.ConnectionStringSettingsCollection collection of System.Configuration.ConnectionStringSettings
        //     objects.
        public ConnectionStringSettingsCollection ConnectionStrings { get; }
        protected internal override ConfigurationPropertyCollection Properties { get; }

        protected internal override object GetRuntimeObject();
    }
}
