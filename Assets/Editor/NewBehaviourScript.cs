using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript
{
	
	public static List<string> GetList()
	{
		List<string> result = new List<string>();
		string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string UnityEditorLogfile = LocalAppData + "\\Unity\\Editor\\Editor.log";
		try
		{
			// Have to use FileStream to get around sharing violations!
			FileStream FS = new FileStream(UnityEditorLogfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			StreamReader SR = new StreamReader(FS);
			
			string line;
			while (!SR.EndOfStream && !(line = SR.ReadLine()).Contains("***Player size statistics***"));
			while (!SR.EndOfStream && !(line = SR.ReadLine()).Contains("Used Assets,"));
			while (!SR.EndOfStream && (line = SR.ReadLine()) != "")
			{
				line = line.Substring(line.IndexOf("% ") + 2);
				result.Add(line);
			}
		}
		catch (Exception E)
		{
			Debug.LogError("Error: " + E);
		}
		return result;
	}
}