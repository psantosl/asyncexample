Async Example

Very simple example to illustrate how async IO helps reducing the response time using less threads.

== Output on single core machine - using async ==

req0 - thId - 3 - Starting request
req0 - thId - 3 - Do heavy calculation ...
req0 - thId - 3 - Done. 1047 ms
req0 - thId - 3 - Sleeping to do async
req1 - thId - 3 - Starting request
req1 - thId - 3 - Do heavy calculation ...
req1 - thId - 3 - Done. 1031 ms
req1 - thId - 3 - Sleeping to do async
req2 - thId - 3 - Starting request
req2 - thId - 3 - Do heavy calculation ...
req2 - thId - 3 - Done. 1016 ms
req2 - thId - 3 - Sleeping to do async
req3 - thId - 3 - Starting request
req3 - thId - 3 - Do heavy calculation ...
req3 - thId - 3 - Done. 1015 ms
req3 - thId - 3 - Sleeping to do async
req0 - thId - 3 - Big async operation. Request terminated. 11125 ms since start to complete
req1 - thId - 3 - Big async operation. Request terminated. 12141 ms since start to complete
req2 - thId - 3 - Big async operation. Request terminated. 13156 ms since start to complete
req3 - thId - 3 - Big async operation. Request terminated. 14187 ms since start to complete


== Output on single core machine - using blocking Sleep ==
req0 - thId - 3 - Starting request
req0 - thId - 3 - Do heavy calculation ...
req0 - thId - 3 - Done. 1031 ms
req0 - thId - 3 - Sleeping to do async
req0 - thId - 3 - Big async operation. Request terminated. 11063 ms since start to complete
req1 - thId - 3 - Starting request
req1 - thId - 3 - Do heavy calculation ...
req1 - thId - 3 - Done. 1016 ms
req1 - thId - 3 - Sleeping to do async
req1 - thId - 3 - Big async operation. Request terminated. 22094 ms since start to complete
req2 - thId - 3 - Starting request
req2 - thId - 3 - Do heavy calculation ...
req2 - thId - 3 - Done. 1031 ms
req2 - thId - 3 - Sleeping to do async
req2 - thId - 3 - Big async operation. Request terminated. 33141 ms since start to complete
req3 - thId - 3 - Starting request
req3 - thId - 3 - Do heavy calculation ...
req3 - thId - 3 - Done. 1016 ms
req3 - thId - 3 - Sleeping to do async
req3 - thId - 3 - Big async operation. Request terminated. 44172 ms since start to complete
