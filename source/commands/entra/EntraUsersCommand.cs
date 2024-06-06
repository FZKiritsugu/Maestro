﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro
{
    internal static class EntraUsersCommand
    {
        public static async Task Execute(Dictionary<string, string> arguments, IDatabaseHandler database, bool databaseOnly = false)
        {
            EntraClient entraClient = new EntraClient();
            if (!databaseOnly)
            {
                entraClient = await EntraClient.CreateAndGetToken(database);
            }

            // Set default properties to print
            string[] properties = new string[] { };

            // User-specified properties
            if (arguments.TryGetValue("--properties", out string propertiesCsv))
            {
                properties = propertiesCsv.Split(',');
            }

            // Filter objects
            if (arguments.TryGetValue("--id", out string userId))
            {
                if (databaseOnly)
                {
                    //entraClient.ShowUsers(database, properties, userId: userId);
                    return;
                }
                //await entraClient.GetUsers(userId: userId, properties: properties, database: database);
            }
            else
            {
                // Get information from all devices by default when no options are provided
                if (databaseOnly)
                {
                    //entraClient.ShowUsers(database, properties);
                    return;
                }
                //await entraClient.GetUsers(properties: properties, database: database);
            }
        }
    }
}
