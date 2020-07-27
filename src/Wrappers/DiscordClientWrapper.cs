﻿using System;
using System.Threading.Tasks;

namespace Victoria.Wrappers {
    /// <summary>
    /// 
    /// </summary>
    public struct DiscordClientWrapper {
        /// <summary>
        /// 
        /// </summary>
        public int Shards { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public ulong UserId { get; }

        /// <summary>
        /// 
        /// </summary>
        public event Func<VoiceServerWrapper, Task> OnVoiceServerUpdated;
    }
}