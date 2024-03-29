﻿using App.Views;

namespace App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("/VoiceDetector", typeof(VoiceDetectorView));
            Routing.RegisterRoute("/Notes", typeof(NotesView));
            Routing.RegisterRoute("/Settings", typeof(SettingsView));
            Routing.RegisterRoute("/Note", typeof(NoteView));
        }
    }
}
