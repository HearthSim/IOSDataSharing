//
//  ViewController.swift
//  HSReplayClient
//
//  Created by Istvan Fehervari on 05/05/2017.
//  Copyright © 2017 Istvan Fehervari. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

	@IBOutlet var textView: UITextView!
	
	override func viewDidLoad() {
		super.viewDidLoad()
		// Do any additional setup after loading the view, typically from a nib.
	}

	override func didReceiveMemoryWarning() {
		super.didReceiveMemoryWarning()
		// Dispose of any resources that can be recreated.
	}

	/// Callback when UI button is pressed
	@IBAction func readPowerLog(sender: UIButton) {
		if let powerlog = readPowerLogViaGeneralPasteBoard() {
			self.textView.text = powerlog
		}
	}
	
	/// Attempts to read the content of the general pasteboard
	private func readPowerLogViaGeneralPasteBoard() -> String? {
		if let textData = UIPasteboard.general.data(forPasteboardType: "com.blizzard.hearthstone.powerlog"), let powerlog = String(data: textData, encoding: .unicode)  {
			return powerlog
		}
		return nil
	}
}

