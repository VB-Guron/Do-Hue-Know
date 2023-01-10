import librosa
import soundfile as sf
import numpy as np
from dtw import dtw
from numpy.linalg import norm
from scipy.io import wavfile
import noisereduce as nr

def calc_Green():
    y1, sr1 = librosa.load('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/girl_voicelines/02. Amplified/Green.wav')  # Color library - do not move
    y2, sr2 = librosa.load('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/VoiceInput.wav', sr=44100)  # Put the unity voice-line file here

    # load data
    rate, data =  wavfile.read('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/VoiceInput.wav')
    # perform noise reduction
    reduced_noise = nr.reduce_noise(y=data, sr=rate)
    wavfile.write("D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/NewNoiseReduction.wav", rate, reduced_noise)

    y5, sr5 = librosa.load("D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/NewNoiseReduction.wav", sr=None)

    S_full, phase = librosa.magphase(librosa.stft(y2))
    S_filter = librosa.decompose.nn_filter(S_full,
                                        aggregate=np.median,
                                        metric='cosine',
                                        width=int(librosa.time_to_frames(2, sr=sr2)))
    S_filter = np.minimum(S_full, S_filter)
    margin_i, margin_v = 2, 10
    power = 2

    mask_i = librosa.util.softmask(S_filter,
                                margin_i * (S_full - S_filter),
                                power=power)

    mask_v = librosa.util.softmask(S_full - S_filter,
                                margin_v * S_filter,
                                power=power)
    S_foreground = mask_v * S_full
    S_background = mask_i * S_full
    new_y = librosa.istft(S_foreground*phase)
    sf.write('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/FilteredAudio.wav', new_y, sr2)

    y3, sr3 = librosa.load('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/FilteredAudio.wav',sr=None)

    yt, index = librosa.effects.trim(y5, top_db=20, frame_length=400, hop_length=124)
    sf.write('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/TrimmedFinalAudio.wav', yt, sr3)

    y4, sr4 = librosa.load('D:/Unity Projects/ThesisClone - Copy/Assets/Final_DTW_Thesis/TrimmedFinalAudio.wav', sr=None)
    mfcc1 = librosa.feature.mfcc(y1, sr1)
    mfcc2 = librosa.feature.mfcc(y4, sr4)

    dist, cost, acc_cost, path = dtw(mfcc1.T, mfcc2.T, dist=lambda x, y: norm(x - y, ord=1))
    print('Normalized distance between the two sounds:', dist)
    if dist <= 20000.00:
        #print("The same")
        return "same, " + str(dist)
    else:
        #print("Different")
        return "different, " + str(dist)