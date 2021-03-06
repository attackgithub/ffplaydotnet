# PLEASE DO NOT USE THIS REPO
The codebase has been moved to: https://github.com/unosquare/ffmediaelement
I will continue development there.

# ffplaydotnet
An attempt to port FFmpeg's FFplay.c

Release: https://github.com/FFmpeg/FFmpeg/releases/tag/n3.2

Source Code: https://github.com/FFmpeg/FFmpeg/blob/release/3.2/ffplay.c


## Media Container Architecture

Media Containers represent an input context, stream operations and decoding logic. The steps to successfully obtain usable datta are as follows:

 - Open the container with a URL.
 - Read the next packet and determine its media type
 - Push the packet into the pending packet queue of the appropriate audio, video or subtitle component
 - Decode packet from the pending packet queue into a frame. Move the packet to the sent packet queue.
 - If 1 or more frames were decoded, clear the sent packets queue.
 - Push the decoded frames into the component's frame queue
 - Dequeue the raw frame from the component's frame queue
 - Materialize the frame into a Media Frame (this converts the raw frame data into usable media)
 - Release (Dispose) the materialized frame


Basic Test Cases:
 - Audio and Video times both start at 0
 - Video frames match end times with the following Video frame's start time
 - Audio frames match end times with the following Audio frame's start time
 - Video frame duration matches framerate
 - Audio frame duration matches start time and end time comparted to the following audio frame
 
