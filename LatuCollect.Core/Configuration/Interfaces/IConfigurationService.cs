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
║  - Charger la configuration utilisateur                              ║
║  - Sauvegarder la configuration                                      ║
║  - Réinitialiser la configuration                                    ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Contrat uniquement                                                ║
║  - Aucune logique métier                                             ║
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
        //
        // CONTRAT :
        // - Ne retourne jamais null
        // - Retourne toujours une configuration valide
        // - Peut retourner une configuration par défaut si échec
        //

        Task<UserConfig> LoadAsync();


        // ═════════════════════════════════════════════════════════════
        // 2. SAUVEGARDE CONFIGURATION
        // ═════════════════════════════════════════════════════════════
        //
        // Sauvegarde la configuration utilisateur
        //
        // CONTRAT :
        // - config ne doit jamais être null
        // - Doit gérer les erreurs (I/O, accès disque, etc.)
        //

        Task SaveAsync(UserConfig config);


        // ═════════════════════════════════════════════════════════════
        // 3. RESET CONFIGURATION
        // ═════════════════════════════════════════════════════════════
        //
        // Réinitialise la configuration avec les valeurs par défaut
        //
        // CONTRAT :
        // - Remplace la configuration actuelle
        // - Sauvegarde immédiatement la nouvelle configuration
        // - Retourne la configuration réinitialisée
        //

        Task<UserConfig> ResetAsync();
    }
}