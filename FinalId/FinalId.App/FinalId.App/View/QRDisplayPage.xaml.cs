﻿// <copyright file="QRDisplayPage.xaml.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.View
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRDisplayPage : ContentPage
    {
        public QRDisplayPage()
        {
            InitializeComponent();

            barcodeImageView.BarcodeOptions.Width = 400;
            barcodeImageView.BarcodeOptions.Height = 400;
        }
    }
}