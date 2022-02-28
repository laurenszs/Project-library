using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QrGenerator : MonoBehaviour
{

    [SerializeField] String qrText;
    private Texture2D QrCode;
    [SerializeField] String port;

    private void Awake()
    {
        QrCode = generateQR(GetExternalIPAddress());
        RawImage qrImage = GetComponent<RawImage>();
        qrImage.texture = QrCode;

    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    #region Ip-info
    public string GetLocalIPv4()// creates a string of your IPv4
    {
        return Dns.GetHostEntry(
            Dns.GetHostName()).AddressList.First(
            f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
    public string GetExternalIPAddress()
    {
        string externalip = new WebClient().DownloadString("https://api.ipify.org/");
        Debug.Log(externalip);
        return externalip;
    }
    #endregion
}