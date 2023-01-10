import librosa
import soundfile as sf
import numpy as np
from dtw import dtw
from numpy.linalg import norm
from scipy.io import wavfile
import noisereduce as nr
from pathlib import Path
import warnings
warnings.simplefilter(action='ignore', category=FutureWarning)



def calc_Blue():
    data_folder =Path(__file__).parent.parent.parent / "girl_voicelines" / "02. Amplified" / "Blue.wav"

    y1, sr1 = librosa.load(data_folder)  # Color library - do not move
    y2, sr2 = librosa.load(Path(__file__).parent.parent.parent / "VoiceInput.wav", sr=44100)  # Put the unity voice-line file here


    yt, index = librosa.effects.trim(y2, top_db=20, frame_length=400, hop_length=124)
    sf.write(Path(__file__).parent.parent.parent /'TrimmedFinalAudio.wav', yt, sr2)

    y4, sr4 = librosa.load(Path(__file__).parent.parent.parent /'TrimmedFinalAudio.wav', sr = None)
    mfcc1 = librosa.feature.mfcc(y1, sr1)
    mfcc2 = librosa.feature.mfcc(y4, sr4) #my voice

    dist, cost, acc_cost, path = dtw(mfcc1.T, mfcc2.T, dist=lambda x, y: norm(x - y, ord=1))
    print('Original:', dist)
    if dist <= 15000.00:
        return "same, " + str(dist)
    else:
        return "different, " + str(dist)
    