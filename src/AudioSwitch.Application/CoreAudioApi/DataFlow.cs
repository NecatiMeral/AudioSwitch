/*
  LICENSE
  -------
  Copyright (C) 2007 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/
// Adapted for AudioSwitch

namespace AudioSwitch.AudioSwitch.CoreAudioApiApi
{
    /// <summary>
    /// The <see cref="DataFlow"/> enumeration defines constants that indicate the direction 
    /// in which audio data flows between an audio endpoint device and an application
    /// </summary>
    public enum DataFlow
    {
        /// <summary>
        /// Audio rendering stream. 
        /// Audio data flows from the application to the audio endpoint device, which renders the stream.
        /// </summary>
        Render = 0,
        /// <summary>
        /// Audio capture stream. Audio data flows from the audio endpoint device that captures the stream, 
        /// to the application
        /// </summary>
        Capture = 1,
        /// <summary>
        /// Audio rendering or capture stream. Audio data can flow either from the application to the audio 
        /// endpoint device, or from the audio endpoint device to the application.
        /// </summary>
        All = 2
    }
}
