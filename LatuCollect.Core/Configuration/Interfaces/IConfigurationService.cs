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
        // ═════════════════════════════════════════════════════════════
        // 1. CHARGEMENT CONFIGURATION
        // ═════════════════════════════════════════════════════════════
        //
        // Charge la configuration utilisateur depuis le stockage
        // Retourne toujours une configuration valide
        //

        Task<UserConfig> LoadAsync();


        // ═════════════════════════════════════════════════════════════
        // 2. SAUVEGARDE CONFIGURATION
        // ═════════════════════════════════════════════════════════════
        //
        // Sauvegarde la configuration utilisateur
        //

        Task SaveAsync(UserConfig config);


        // ═════════════════════════════════════════════════════════════
        // 3. RESET CONFIGURATION
        // ═════════════════════════════════════════════════════════════
        //
        // Réinitialise la configuration avec les valeurs par défaut
        //

        Task<UserConfig> ResetAsync();
    }
}