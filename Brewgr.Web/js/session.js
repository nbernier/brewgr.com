

$(function () {

    // Track Form Changes
    $('#brew-session-form input, #brew-session-form select, #brew-session-form textarea').not('#taste-date').not('#NewTastingNotes').not('.CommentText').change(function () {
        if (!$('#brew-session-form').attr('data-formchanged')) {
            // add the data-formchanged to the form
            $('#brew-session-form').attr('data-formchanged', 'true');
        }
    });

    // Warn of Lost Changes
    $(window).bind('beforeunload', function () {
        if ($('#brew-session-form[data-formchanged=true]').length > 0) {
            return 'You have unsaved changes';
        }
    });

    $('body').on('click', '.delete-brewsession', function (e) {
        $.colorbox({ html: '<div style="padding: 12px 24px 0 24px;"><h3>Are you sure you want to delete this?</h3><p>Once you delete a brew session there is no way to get it back.<br /><a class="btn btn-danger btn-md" href="/brew/' + $(this).attr("data-brewsessionid") + '/delete">Yes, Delete</a>&nbsp;<a class="btn btn-default btn-md" href="#" onclick="$.colorbox.close()">No, don\'t delete</a><p/></div>', opacity: .35, overlayClose: false, escKey: false, scrolling: false });
        return false;
    });

    // Date Picker
    $('.datepicker').datepicker();

    SessionBuilder.initialize();

});

// -------------------------------------------------------------------------------------------------------
// Recipe Builder Static Class
// -------------------------------------------------------------------------------------------------------
var SessionBuilder =
    {
        initialize: function () {
           
            // Notes Autosize
            $('textarea[data-name=s_Notes]').autosize();
            
            // Wire Save Button
            $('.SaveBrewSessionButton').click(function () {
                SessionBuilder.save();
            });
        },

        // Gets the session as JSON
        getSession: function () {
            return {
                BrewSessionId: SessionBuilder.getValue('[data-name=s_BrewSessionId]'),
                RecipeId: SessionBuilder.getValue('[data-name=s_RecipeId]'),
                UnitTypeId: SessionBuilder.getValue('[data-name=s_UnitType]'),
                BrewDate: SessionBuilder.getValue('[data-name=s_BrewDate]'),
                Notes: SessionBuilder.getValue('[data-name=s_Notes]'),
                GrainWeight: SessionBuilder.getValue('[data-name=s_GrainWeight]'),
                GrainTemp: SessionBuilder.getValue('[data-name=s_GrainTemp]'),
                BoilTime: SessionBuilder.getValue('[data-name=s_BoilTime]'),
                BoilVolumeEst: SessionBuilder.getValue('[data-name=s_BoilVolumeEst]'),
                FermentVolumeEst: SessionBuilder.getValue('[data-name=s_FermentVolume]'),
                TargetMashTemp: SessionBuilder.getValue('[data-name=s_TargetMashTemp]'),
                MashThickness: SessionBuilder.getValue('[data-name=s_MashThickness]'),
                TotalWaterNeeded: SessionBuilder.getValue('[data-name=s_TotalWaterNeeded]'),
                StrikeWaterTemp: SessionBuilder.getValue('[data-name=s_StrikeWaterTemp]'),
                StrikeWaterVolume: SessionBuilder.getValue('[data-name=s_StrikeWaterVolume]'),
                MashTunCapacity: SessionBuilder.getValue('[data-name=s_MashTunCapacity]'),
                FirstRunningsVolume: SessionBuilder.getValue('[data-name=s_FirstRunningsVolume]'),
                SpargeWaterVolume: SessionBuilder.getValue('[data-name=s_SpargeWaterVolume]'),
                BrewKettleLoss: SessionBuilder.getValue('[data-name=s_BrewKettleLoss]'),
                WortShrinkage: SessionBuilder.getValue('[data-name=s_WortShrinkage]'),
                MashTunLoss: SessionBuilder.getValue('[data-name=s_MashTunLoss]'),
                HLTMTHeatLoss: SessionBuilder.getValue('[data-name=s_HLT_MT_HeatLoss]'),
                BoilLoss: SessionBuilder.getValue('[data-name=s_BoilLoss]'),
                MashGrainAbsorption: SessionBuilder.getValue('[data-name=s_MashGrainAbsorption]'),
                SpargeGrainAbsorption: SessionBuilder.getValue('[data-name=s_SpargeGrainAbsorption]'),
                MashPH: SessionBuilder.getValue('[data-name=s_MashPH]'),
                MashStartTemp: SessionBuilder.getValue('[data-name=s_MashStartTemp]'),
                MashEndTemp: SessionBuilder.getValue('[data-name=s_MashEndTemp]'),
                MashTime: SessionBuilder.getValue('[data-name=s_MashTime]'),
                BoilVolumeActual: SessionBuilder.getValue('[data-name=s_BoilVolumeActual]'),
                PreBoilGravity: SessionBuilder.getValue('[data-name=s_PreBoilGravity]'),
                BoilTimeActual: SessionBuilder.getValue('[data-name=s_BoilTimeActual]'),
                PostBoilVolume: SessionBuilder.getValue('[data-name=s_PostBoilVolume]'),
                FermentVolumeActual: SessionBuilder.getValue('[data-name=s_FermentVolumeActual]'),
                OriginalGravity: SessionBuilder.getValue('[data-name=s_OriginalGravity]'),
                FinalGravity: SessionBuilder.getValue('[data-name=s_FinalGravity]'),
                ConditionDate: SessionBuilder.getValue('[data-name=s_ConditionDate]'),
                ConditionTypeId: SessionBuilder.getValue('[data-name=s_ConditionType]'),
                PrimingSugarType: SessionBuilder.getValue('[data-name=s_PrimingSugarType]'),
                PrimingSugarAmount: SessionBuilder.getValue('[data-name=s_PrimingSugarAmount]'),
                KegPSI: SessionBuilder.getValue('[data-name=s_KegPSI]')
            };
        },

        // Gets a value from a selector
        getValue: function (selector) {
            return $(selector).length ? $(selector).val() : null;
        },

        // Uses a unit type
        //useUnit: function (unit) {
        //    if ($('[data-name=s_UnitType]') === unit) {
        //        return;
        //    }

        //    var prevUnit = unit === 'm' ? 's' : 'm';

        //    // Toggle Labels
        //    $('[data-unit].active').removeClass('active');
        //    $('[data-unit="' + unit + '"]').addClass('active');

        //    util.convert('[data-name=s_MashStartTemp]', prevUnit, util.c_To_f, util.f_To_c);
        //    util.convert('[data-name=s_MashEndTemp]', prevUnit, util.c_To_f, util.f_To_c);

        //    util.convert('[data-name=s_BoilVolumeActual]', prevUnit, util.l_To_gal, util.gal_To_l);
        //    util.convert('[data-name=s_PostBoilVolume]', prevUnit, util.l_To_gal, util.gal_To_l);
        //    util.convert('[data-name=s_FermentVolumeActual]', prevUnit, util.l_To_gal, util.gal_To_l);
        //    util.convert('[data-name=s_PrimingSugarAmount]', prevUnit, util.g_To_oz, util.oz_To_g);

        //    util.convert('[data-name=p_BoilVolumeEst]', prevUnit, util.l_To_gal, util.gal_To_l);
        //},

        /// Saves the Brew Session
        save: function () {

            // So the "You have unsaved changes" doesn't show
            $('#brew-session-form').attr('data-formchanged', 'false');

            // Clear Messages
            Message.clear();

            var session = SessionBuilder.getSession();

            // Validate
            $('.session .field-error').removeClass('field-error');
            if ($('[data-name=s_BrewDate]').val() == null || $('[data-name=s_BrewDate]').val().length == 0) {
                $('[data-name=s_BrewDate]').addClass('field-error');
                Message.error('Uh oh, something needs your attention.  Please check the highlighted entries below.');
                window.scrollTo(0, 1);
                return false;
            }

            try {
                var brew_date = new Date($('[data-name=s_BrewDate]').val());
                var brew_date_today = new Date();
                if (brew_date > brew_date_today.addDays(14)) {
                    $('[data-name=s_BrewDate]').addClass('field-error');
                    Message.error('Uh oh, something needs your attention. Brew date cannot be that far in the future.');
                    window.scrollTo(0, 1);
                    return false;
                }
            }
            catch (err) {
                Message.error('Uh oh, something went wrong.');
                window.scrollTo(0, 1);
                return false;
            }

            // Save 
            if (session.BrewSessionId == 0) {
                // New Session Saved via Form POST
                Layout.statusModal('Saving Brew Session...');
                $('.builder').removeAttr('data-formchanged');
                $('#SessionJson').val(escape(JSON.stringify(session)));
                $('#SessionForm').submit();
            } else {
                // Existing Session Saved via Ajax
                // We execute the AJAX request after the modal to ensure
                // that the "Saving Brew Session" message is displayed first
                var modalComplete = function () {
                    $.ajax({
                        url: "/brew/SaveSession",
                        data: { SessionJson: JSON.stringify(session) },
                        method: "post",
                        success: function (t) {
                            switch (t) {
                                case "-1": // general failure
                                    Message.error('There was a problem saving your brew session.  Please try again');
                                    break;
                                case "0": // validation error
                                    Message.error('Did you leave something blank?  Please check your entries and try again.');
                                    break;
                                case "1": // success
                                    Message.success('Your Brew Session has been saved');
                                    $('.builder').removeAttr('data-formchanged');
                                    break;
                            }
                        },
                        error: function (t) {
                            Message.error('There was a problem saving your brew session.  Please try again.');
                        },
                        complete: function () {
                            $.colorbox.close();

                            // Prevent the Facts from Stepping on the Message
                            $('.compliment').css('position', '').css('margin-left', '').css('top', '');
                            window.scrollTo(0, 0);
                        }
                    });
                };

                Layout.statusModal('Saving Brew Session...', modalComplete);
            }

            return false;
        }
    };