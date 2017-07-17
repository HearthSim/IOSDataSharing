using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Threading;

public class SimpleServerScript : MonoBehaviour {

	private HttpListener m_listener = null;
	private Thread m_listenerThread;

	// define server port
	public const int serverPort = 9999;

	// indicates whether the server is running
	private bool m_isRunning = false;

	// Use this for initialization
	void Awake () {
	}

	void Start ()
	{
		Setup();
	}

	/* Starts the server */
	void Setup() {
		m_listener = new HttpListener();

		m_listener.Prefixes.Add("http://localhost:" + serverPort + "/powerlog/");

		// Listening should run on a separate thread to avoid blocking the unity update thread
		m_listener.Start();
		m_listenerThread = new Thread(new ThreadStart(this.Listen));
		m_listenerThread.Start();
	}

	/* Update is called once per frame */
	void Update () {
		// Do nothing, listener is on its own thread
	}

	void Listen() {
		this.m_isRunning = true;
		while (this.m_isRunning) {
			// Note: The GetContext method blocks while waiting for a request. 
			HttpListenerContext context = m_listener.GetContext();

			// On a connection a respone thread is spawned
			Thread responseThread = new Thread(() => Respond(context));
			responseThread.Start ();
		}
	}

	/* Respond to an http request */
	void Respond(HttpListenerContext context) {
		// The request object has data about the exact request URI data
		HttpListenerRequest request = context.Request;

		// Only request from localhost are handled
		if (!request.IsLocal) {
			return;
		}
		
		// Obtain a response object.
		HttpListenerResponse response = context.Response;
		
		// Construct a response.
		string responseString = "<HTML><BODY> Power log content comes here </BODY></HTML>";

		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		
		// Get a response stream and write the response to it.
		response.ContentLength64 = buffer.Length;
		System.IO.Stream output = response.OutputStream;
		output.Write(buffer,0,buffer.Length);
		
		// The output stream must be closed
		output.Close();
	}

	/* Stops the server */
	void Stop() {
		this.m_isRunning = false;
	}
}
