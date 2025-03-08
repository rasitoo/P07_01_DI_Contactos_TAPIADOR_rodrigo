﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P07_01_DI_Contactos_TAPIADOR_rodrigo.Data.Entities;
using P07_01_DI_Contactos_TAPIADOR_rodrigo.Data.Rest;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace P07_01_DI_Contactos_TAPIADOR_rodrigo.PageModels;

public partial class PlaylistsPageModel : ObservableObject
{
    private IRestClient<Playlist> _playlistRestClient;
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();
    [ObservableProperty]
    private bool _playlistsLoading = true;
    [ObservableProperty]
    private Playlist? _selectedPlaylist = new();
    async partial void OnSelectedPlaylistChanged(Playlist value)
    {
        if (value != null)
        {
            await Shell.Current.GoToAsync($"playlist?id={value.Id}");
            SelectedPlaylist = null;
        }
    }
    public PlaylistsPageModel(IRestClient<Playlist> playlistRestClient)
    {
        _playlistRestClient = playlistRestClient;
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        List<Playlist> playlists = await _playlistRestClient.GetAll(0, 100);
        LoadPlaylists(playlists);
    }

    private void LoadPlaylists(List<Playlist> playlists)
    {
        Playlists = new(playlists);
        PlaylistsLoading = false;
    }

    [RelayCommand]
    private async void CreatePlaylist()
    {
        await Shell.Current.GoToAsync($"playlist?id=0");
    }
}
