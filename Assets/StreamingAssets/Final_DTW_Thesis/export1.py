import pathlib
data_folder = pathlib.Path(__file__).parent / "girl_voicelines" / "02_Aplified" / "Red.wav"
print(data_folder)
print(pathlib.Path(__file__).parent)
print(pathlib.Path(__file__).parent.parent.parent / "girl_voicelines" / "02_Aplified" / "Red.wav") 
input("Press Somethings ")