pipeline{
    agent any
    tools {
        dotnetsdk '.NET 5'
    }
    
    stages {
        stage('Build'){
            steps{
                sh 'dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained false'
            }
            post{
                success{
                      archiveArtifacts artifacts: 'ProjectEarthServerAPI/bin/Debug/net5.0/linux-x64/publish/ProjectEarthServerAPI'
                      archiveArtifacts artifacts: 'ProjectEarthServerAPI/bin/Debug/net5.0/linux-x64/publish/ProjectEarthServerAPI.pdb'
                }
            }
        }
    }

     post {
        always {
            script {
                def changeLogSets = currentBuild.changeSets
                def message = "**Changes:**"

                if (changeLogSets.size() == 0) {
                    message += "\n*No changes.*"
                } else {
                    def repositoryUrl = scm.userRemoteConfigs[0].url.replace(".git", "")
                    def count = 0;
                    def extra = 0;
                    for (int i = 0; i < changeLogSets.size(); i++) {
                        def entries = changeLogSets[i].items
                        for (int j = 0; j < entries.length; j++) {
                            if (count <= 10) {
                                def entry = entries[j]
                                def commitId = entry.commitId.substring(0, 6)
                                message += "\n   - [`${commitId}`](${repositoryUrl}/commit/${entry.commitId}) ${entry.msg}"
                                count++
                            } else {
                                extra++;
                            }
                        }
                    }
                    
                    if (extra != 0) {
                        message += "\n   - ${extra} more commits"
                    }
                }

                env.changes = message
            }
            deleteDir()
            withCredentials([string(credentialsId: 'projectearth-discord-webhook', variable: 'DISCORD_WEBHOOK')]) {
                discordSend description: "**Build:** [${currentBuild.id}](${env.BUILD_URL})\n**Status:** [${currentBuild.currentResult}](${env.BUILD_URL})\n${changes}\n\n[**Artifacts on Jenkins**](https://ci.rtm516.co.uk/job/ProjectEarth/job/Api/job/master/)", footer: 'rtm516\'s Jenkins', link: env.BUILD_URL, successful: currentBuild.resultIsBetterOrEqualTo('SUCCESS'), title: "${env.JOB_NAME} #${currentBuild.id}", webhookURL: DISCORD_WEBHOOK
            }
        }
    }
}
