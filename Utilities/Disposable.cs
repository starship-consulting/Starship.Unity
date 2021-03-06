﻿using System;

namespace Starship.Unity.Utilities {
    public abstract class Disposable : IDisposable {
        public void Dispose() {
            if (!IsDisposed) {
                IsDisposed = true;
                Disposed();
            }
        }

        public abstract void Disposed();

        private bool IsDisposed { get; set; }
    }
}