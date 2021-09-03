// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Google.Play.Billing.Editor
{
    /// <summary>
    /// Helper class to address building issues of Google Play Billing Plugin.
    /// </summary>
    public class GooglePlayBillingBuildHelper
    {
        private const string UnityIapGoogleAndroidAarPath = "Assets/Plugins/UnityPurchasing/Bin/Android";

        private const string UnityIapGoogleAndroidAarBackupPath = "Assets/GooglePlayBillingBackup~";

        private const string UnityIapGoogleAidlAarFilename = "GoogleAIDL.aar";
        private const string UnityIapGoogleAidlAarMetaDataFilename = "GoogleAIDL.aar.meta";
        private const string UnityIapGooglePlayBillingAarFilename = "GooglePlayBilling.aar";
        private const string UnityIapGooglePlayBillingAarMetaDataFilename = "GooglePlayBilling.aar.meta";

        private static readonly string UnityIapGoogleAidlAar =
            Path.Combine(UnityIapGoogleAndroidAarPath, UnityIapGoogleAidlAarFilename);

        private static readonly string UnityIapGoogleAidlAarBackup = Path.Combine(
            UnityIapGoogleAndroidAarBackupPath, UnityIapGoogleAidlAarFilename);

        private static readonly string UnityIapGoogleAidlAarMetaData =
            Path.Combine(UnityIapGoogleAndroidAarPath, UnityIapGoogleAidlAarMetaDataFilename);

        private static readonly string UnityIapGoogleAidlAarMetaDataBackup = Path.Combine(
            UnityIapGoogleAndroidAarBackupPath, UnityIapGoogleAidlAarMetaDataFilename);

        private static readonly string UnityIapGooglePlayBillingAar =
            Path.Combine(UnityIapGoogleAndroidAarPath, UnityIapGooglePlayBillingAarFilename);

        private static readonly string UnityIapGooglePlayBillingAarBackup = Path.Combine(
            UnityIapGoogleAndroidAarBackupPath, UnityIapGooglePlayBillingAarFilename);

        private static readonly string UnityIapGooglePlayBillingAarMetaData =
            Path.Combine(UnityIapGoogleAndroidAarPath, UnityIapGooglePlayBillingAarMetaDataFilename);

        private static readonly string UnityIapGooglePlayBillingAarMetaDataBackup = Path.Combine(
            UnityIapGoogleAndroidAarBackupPath, UnityIapGooglePlayBillingAarMetaDataFilename);

        /// <summary>
        /// Returns whether there is a conflicting aar file that will cause failure when building the APK.
        /// </summary>
        public static bool HasConflictingGoogleAarFile()
        {
            return HasConflictingGoogleAidlFile() || HasConflictingGooglePlayBillingAarFile();
        }

        /// <summary>
        /// Returns whether there is a conflicting GoogleAIDL.aar file that will cause failure when building the APK.
        /// </summary>
        private static bool HasConflictingGoogleAidlFile()
        {
            return File.Exists(UnityIapGoogleAidlAar);
        }

        /// <summary>
        /// Returns whether there is a conflicting GooglePlayBilling.aar file that will cause failure when building the
        /// APK.
        /// </summary>
        private static bool HasConflictingGooglePlayBillingAarFile()
        {
            return File.Exists(UnityIapGooglePlayBillingAar);
        }

        /// <summary>
        /// Remove the conflicting GoogleAIDL.aar and GooglePlayBilling.aar from Assets scope by moving it to a backup
        /// directory.
        /// </summary>
        /// <returns> true if operation succeed, otherwise false. </returns>
        /// TODO (b/145928532): display error message on UI.
        public static bool RemoveConflictingAarFiles()
        {
            if (!HasConflictingGoogleAidlFile() && !HasConflictingGooglePlayBillingAarFile())
            {
                return true;
            }

            try
            {
                Directory.CreateDirectory(UnityIapGoogleAndroidAarBackupPath);
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.LogErrorFormat(
                    "Permission denied when creating backup directory {0}. Please manually create the directory. Exception: {1}",
                    UnityIapGoogleAndroidAarBackupPath, ex);
                return false;
            }

            if (HasConflictingGoogleAidlFile())
            {
                try
                {
                    // Delete the files before moving. The operation is NO-OP if the file doesn't exist.
                    File.Delete(UnityIapGoogleAidlAarBackup);
                    File.Delete(UnityIapGoogleAidlAarMetaDataBackup);
                    File.Move(UnityIapGoogleAidlAar, UnityIapGoogleAidlAarBackup);
                    File.Move(UnityIapGoogleAidlAarMetaData, UnityIapGoogleAidlAarMetaDataBackup);
                    File.Move(UnityIapGooglePlayBillingAar, UnityIapGooglePlayBillingAarBackup);
                    File.Move(UnityIapGooglePlayBillingAarMetaData, UnityIapGooglePlayBillingAarMetaDataBackup);
                    Debug.LogFormat("Successfully backed up GoogleAIDL.aar to {0}", UnityIapGoogleAndroidAarBackupPath);
                }
                catch (FileNotFoundException)
                {
                    // This could only be the .meta file as we checked for the .aar file in the if. Ignore it.
                    Debug.LogWarningFormat("GoogleAIDL.aar.meta file is not found. Skip backing it up.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.LogErrorFormat(
                        "Permission denied when move GoogleAIDL.aar file to the backup directory. " +
                        "Please check the directory access of {0}. Exception: {1}.",
                        UnityIapGoogleAndroidAarBackupPath, ex);
                    return false;
                }
            }

            if (HasConflictingGooglePlayBillingAarFile())
            {
                try
                {
                    // Delete the files before moving. The operation is NO-OP if the file doesn't exist.
                    File.Delete(UnityIapGooglePlayBillingAarBackup);
                    File.Delete(UnityIapGooglePlayBillingAarMetaDataBackup);
                    File.Move(UnityIapGooglePlayBillingAar, UnityIapGooglePlayBillingAarBackup);
                    File.Move(UnityIapGooglePlayBillingAarMetaData, UnityIapGooglePlayBillingAarMetaDataBackup);
                    Debug.LogFormat("Successfully backed up GooglePlayBilling.aar to {0}", UnityIapGoogleAndroidAarBackupPath);
                }
                catch (FileNotFoundException)
                {
                    // This could only be the .meta file as we checked for the .aar file in the if. Ignore it.
                    Debug.LogWarningFormat("GooglePlayBilling.aar.meta file is not found. Skip backing it up.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.LogErrorFormat(
                        "Permission denied when move GooglePlayBilling.aar file to the backup directory. " +
                        "Please check the directory access of {0}. Exception: {1}.",
                        UnityIapGoogleAndroidAarBackupPath, ex);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Restore the conflicting GoogleAIDL.aar and GooglePlayBilling.aar back to Assets scope by moving it back from
        /// the backup directory.
        /// </summary>
        /// <returns> true if operation succeed, otherwise false. </returns>
        /// TODO (b/145928532): display error message on UI.
        public static bool RestoreConflictingAarFiles()
        {
            if (HasConflictingGoogleAidlFile() && HasConflictingGooglePlayBillingAarFile())
            {
                return true;
            }

            bool fileRestored = false;

            if (File.Exists(UnityIapGoogleAidlAarBackup))
            {
                try
                {
                    File.Move(UnityIapGoogleAidlAarBackup, UnityIapGoogleAidlAar);
                    File.Move(UnityIapGoogleAidlAarMetaDataBackup, UnityIapGoogleAidlAarMetaData);
                    Debug.LogFormat("Successfully restored GoogleAIDL.aar to {0}", UnityIapGoogleAndroidAarPath);
                    fileRestored = true;
                }
                catch (FileNotFoundException)
                {
                    // This can only be the .meta file. It's OK as Unity will regenerate one.
                    Debug.LogWarningFormat("GoogleAIDL.aar.meta file is not found. Skip restoring it up.");
                }
                catch (Exception ex)
                {
                    Debug.LogErrorFormat(
                        "Cannot restore GoogleAIDL.aar file due to exception {0}. Please manually re-import Unity IAP.",
                        ex);
                }
            }
            else
            {
                Debug.LogWarningFormat(
                    "Cannot find the backup GoogleAIDL.aar file. Restore failed! Please manually re-import Unity IAP.");
            }

            if (File.Exists(UnityIapGooglePlayBillingAarBackup))
            {
                try
                {
                    File.Move(UnityIapGooglePlayBillingAarBackup, UnityIapGooglePlayBillingAar);
                    File.Move(UnityIapGooglePlayBillingAarMetaDataBackup, UnityIapGooglePlayBillingAarMetaData);
                    Debug.LogFormat("Successfully restored GooglePlayBilling.aar to {0}", UnityIapGoogleAndroidAarPath);
                    fileRestored = true;
                }
                catch (FileNotFoundException)
                {
                    // This can only be the .meta file. It's OK as Unity will regenerate one.
                    Debug.LogWarningFormat("GooglePlayBilling.aar.meta file is not found. Skip restoring it up.");
                }
                catch (Exception ex)
                {
                    Debug.LogErrorFormat(
                        "Cannot restore GooglePlayBilling.aar file due to exception {0}. Please manually re-import Unity IAP.",
                        ex);
                }
            }
            else
            {
                Debug.LogWarningFormat(
                    "Cannot find the backup GooglePlayBilling.aar file. Restore failed! Please manually re-import Unity IAP.");
            }

            // Only delete the directory if it is empty.
            if (!Directory.GetFileSystemEntries(UnityIapGoogleAndroidAarBackupPath).Any())
            {
                return fileRestored;
            }

            try
            {
                Directory.Delete(UnityIapGoogleAndroidAarBackupPath);
            }
            catch (IOException ex)
            {
                Debug.LogErrorFormat("Cannot delete {0} due to exception {1}.", UnityIapGoogleAndroidAarBackupPath,
                    ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.LogErrorFormat(
                    "Permission denied when deleting {0}. Please manually delete it. Exception: {1}.",
                    UnityIapGoogleAndroidAarBackupPath, ex);
            }

            return fileRestored;
        }
    }
}