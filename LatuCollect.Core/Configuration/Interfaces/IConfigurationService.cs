/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Interfaces                              ║
║  Fichier : IConfigurationService.cs                                  ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définir le contrat du service de configuration                      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Charger la configuration                                          ║
║  - Sauvegarder la configuration                                      ║
║  - Réinitialiser la configuration                                    ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Threading.Tasks;
using LatuCollect.Core.Configuration.Models;

namespace LatuCollect.Core.Configuration.Interfaces
{
    public interface IConfigurationService
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CHARGEMENT
        // ═════════════════════════════════════════════════════════════════════

        Task<UserConfig> LoadAsync();


        // ═════════════════════════════════════════════════════════════════════
        // 2. SAUVEGARDE
        // ═════════════════════════════════════════════════════════════════════

        Task SaveAsync(UserConfig config);


        // ═════════════════════════════════════════════════════════════════════
        // 3. RESET
        // ═════════════════════════════════════════════════════════════════════

        Task<UserConfig> ResetAsync();
    }
}