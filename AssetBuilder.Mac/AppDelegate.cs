﻿using System;
using AppKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using CoreGraphics;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace AssetBuilder.Mac
{
    public class MyWindow : NSWindow
    {
        public MyWindow()
        {
        }

        public MyWindow(NSCoder coder) : base(coder)
        {
        }

        public MyWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation) : base(contentRect, aStyle, bufferingType, deferCreation)
        {
        }

        public MyWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation, NSScreen screen) : base(contentRect, aStyle, bufferingType, deferCreation, screen)
        {
        }

        protected MyWindow(NSObjectFlag t) : base(t)
        {
        }

        protected internal MyWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("showHelp:")]
        private void HandleShowHelp(NSObject sender)
        {
            System.Diagnostics.Debug.WriteLine("MyWindow");
            NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl("https://github.com/baskren/AssetBuilder/issues?q=is%3Aissue+is%3Aclosed"));

        }

        public override void PerformClose(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(sender);
        }

    }

    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        readonly NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(200, 1000, 1024, 768);
            //window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window = new MyWindow(rect, style, NSBackingStore.Buffered, false);

            window.Title = "AssetBuilder for Xamarin"; // choose your own Title here
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow => window;

        public override void DidFinishLaunching(NSNotification notification)
        {
            Microsoft.AppCenter.AppCenter.Start("e2fc0aad-b432-4dd8-872c-13b10b69a1f4", typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));

            Forms.Init();
            P42.Utils.MacOS.Settings.Init();
            P42.SandboxedStorage.Platform.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }


    }
}
