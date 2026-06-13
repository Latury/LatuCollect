/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.ViewModels.Settings                               ║
║  Fichier : SettingsViewModel.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer les paramètres utilisateur                                    ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gestion du thème                                                  ║
║  - Gestion des préférences utilisateur                               ║
║  - Gestion des paramètres UI                                         ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucune logique métier                                             ║
║  - Aucun accès disque                                                ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration;
using LatuCollect.Core.Configuration.Models;
using LatuCollect.Core.Logging.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LatuCollect.UI.WinUI.ViewModels.Settings
{
    public partial class SettingsViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private string _selectedTheme = "Dark";

        private string _selectedLogLevel = "Info";

        private bool _isDeveloperMode;

        private readonly UserConfig _userConfig;

        private readonly AppConfig _config;

        private readonly ILogService _logger;

        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public string SelectedLogLevel
        {
            get => _selectedLogLevel;
            set => SetProperty(ref _selectedLogLevel, value);
        }

        public bool IsDeveloperMode
        {
            get => _isDeveloperMode;
            set => SetProperty(ref _isDeveloperMode, value);
        }

        public bool IsDeveloperModeEnabled
        {
            get => IsDeveloperMode;
            set => IsDeveloperMode = value;
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public SettingsViewModel(
            UserConfig userConfig,
            AppConfig config,
            ILogService logger)
        {
            _userConfig = userConfig;
            _config = config;
            _logger = logger;
        }

        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODES PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public void UpdateDeveloperMode(
            bool value)
        {
            IsDeveloperMode = value;
        }
    }
}