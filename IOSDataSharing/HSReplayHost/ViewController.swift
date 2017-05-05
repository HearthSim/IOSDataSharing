//
//  ViewController.swift
//  IOSDataSharing
//
//  Created by Istvan Fehervari on 05/05/2017.
//  Copyright © 2017 Istvan Fehervari. All rights reserved.
//

import UIKit

class ViewController: UIViewController, UITextViewDelegate {
	
	@IBOutlet var textView: UITextView!

	override func viewDidLoad() {
		super.viewDidLoad()
		
		shareDataViaGeneralPasteBoard(textView.text)
	}

	override func didReceiveMemoryWarning() {
		super.didReceiveMemoryWarning()
		// Dispose of any resources that can be recreated.
	}
	
	/// Callback when the text in the view changes
	func textViewDidChange(_ textView: UITextView) {
		shareDataViaGeneralPasteBoard(textView.text)
	}
	
	/// Shared the given string via the general pasteboard
	private func shareDataViaGeneralPasteBoard(_ data: String) {
		if let textData = data.data(using: .unicode) {
			UIPasteboard.general.setValue(textData, forPasteboardType: "com.blizzard.hearthstone.powerlog")
		}
	}


}

