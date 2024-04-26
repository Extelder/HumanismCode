using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public int ConnectionID;
    public string PlayerName;
    public ulong PlayerSteamID;
    public Text PlayerNameText;
    public RawImage PlayerIcon;

    private bool _avatarReceived;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    private void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoad);
    }

    private void GetPlayerIcon()
    {
        int ImageId = SteamFriends.GetLargeFriendAvatar((CSteamID) PlayerSteamID);
        if (ImageId == -1) return;
        PlayerIcon.texture = GetSteamImageAsTexture(ImageId);
    }

    private void OnImageLoad(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID == PlayerSteamID)
            PlayerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
        else
            return;
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int) (width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int) width, (int) height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }

        _avatarReceived = true;
        return texture;
    }

    public void SetPlayerValues()
    {
        PlayerNameText.text = PlayerName;
        if(!_avatarReceived) GetPlayerIcon();
    }
}