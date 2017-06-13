using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Threading;

public class SimpleServerScript : MonoBehaviour {

	HttpListener listener = null;

	public int serverPort = 9999;
	bool Running = false;

	// Use this for initialization
	void Awake () {
	}

	void Start ()
	{
		//Debug.Log("Start called.");
		Setup();
	}

	void Setup() {
		listener = new HttpListener();

		listener.Prefixes.Add("http://localhost:8080/powerlog/");

		listener.Start();
		Thread listenerThread = new Thread (new ThreadStart(this.Listen));
		listenerThread.Start ();
	}

	// Update is called once per frame
	void Update () {
	}

	void Listen() {
		this.Running = true;
		while (this.Running) {
			// Note: The GetContext method blocks while waiting for a request. 
			HttpListenerContext context = listener.GetContext();

			Thread responseThread = new Thread(() => Respond(context));
			responseThread.Start ();
		}
	}

	void Respond(HttpListenerContext context) {
		HttpListenerRequest request = context.Request;
		// Obtain a response object.
		HttpListenerResponse response = context.Response;
		// Construct a response.
		string responseString = "<HTML><BODY> Power log content comes here </BODY></HTML>";

		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		// Get a response stream and write the response to it.
		response.ContentLength64 = buffer.Length;
		System.IO.Stream output = response.OutputStream;
		output.Write(buffer,0,buffer.Length);
		// You must close the output stream.
		output.Close();
	}


}
