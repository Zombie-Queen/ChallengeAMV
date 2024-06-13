    // Handle navigation clicks
    $('#tourscardsLink').on('click', function (e) {
        loadContent($(this).data('url'));
    });

    $('#toursLink').on('click', function (e) {
        loadContent($(this).data('url'));
    });

    $('#toursReservations').on('click', function (e) {
        loadContent($(this).data('url'));
    });

    $('#login').on('click', function (e) {
        loadContent($(this).data('url'));
    });

    $('#logout').on('click', function (e) {
        loadContent($(this).data('url'));
    });

    $('.btn-login').on('click', function () {
        debugger
        var loginForm = {
            Username: $('#username').val(),
            Password: $('#password').val()
        };

        $.ajax({
            type: 'POST',
            url: $(this).data('url'),
            data: JSON.stringify(loginForm),
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                $('#container').html(response);         
            },
            error: function (error) {
                $('#alerterror').text("Error al iniciar sesión");
                $('#alerterror').fadeIn(1000);
                setTimeout(function () {
                    $('#alerterror').fadeOut(1000);
                }, 3000);
            }
        });
    });

    function loadContent(url) {
        $.ajax({
            url: url,
            type: 'GET',
            success: function (result) {
                debugger
                $('#container div').empty();
                $('#container').html(result);         
            },
            error: function () {
                $('#alerterror').text("Error.");
                $('#alerterror').fadeIn(1000);
                setTimeout(function () {
                    $('#alerterror').fadeOut(1000);
                }, 3000);
            }
        });
    }

        $('.btn-delete-r').on('click', function () {
        
        var rId = $(this).data('reservationid');
        var deleteUrl = $(this).data('deletereservation');        
        debugger
        
        if (confirm("Desea eliminar la reserva?")) {
            
            $.ajax({
                url: deleteUrl + '/' + rId,
                type: 'DELETE',
                success: function (result) {
                    $('#alertok').text("Reserva Eliminada.");
                    $('#alertok').fadeIn(1000);
                    setTimeout(function () {
                        $('#alertok').fadeOut(1000);
                    }, 3000);
                    $.ajax({
                        url: $(this).data('url'),
                        data: { id: parseInt($(this).data('tourid')) },
                        type: 'GET',
                        success: function (result) {
                            $('#container').html(result);
                        },
                        error: function () {
                            alert('Error loading content.');
                        }
                    });
                },
                error: function (xhr, status, error) {
                    $('#alerterror').text("Error al eliminar.");
                    $('#alerterror').fadeIn(1000);
                    setTimeout(function () {
                        $('#alerterror').fadeOut(1000);
                    }, 3000);
                }
            });
        }
    });

        $('.btn-delete').on('click', function () {
        
        var tourId = $(this).data('tourid');
        var deleteUrl = $(this).data('tour');        

        
        if (confirm("Desea eliminar el tour?")) {
            
            $.ajax({
                url: deleteUrl + '/' + tourId,
                type: 'DELETE',
                success: function (result) {
                    debugger
                    $('#alertok').text("Tour Eliminado.");
                    $('#alertok').fadeIn(1000);
                    setTimeout(function () {
                        $('#alertok').fadeOut(1000);
                    }, 3000);
                    loadContent($(this).data('tourlist'));
                },
                error: function (xhr, status, error) {
                    $('#alerterror').text("Error al eliminar.");
                    $('#alerterror').fadeIn(1000);
                    setTimeout(function () {
                        $('#alerterror').fadeOut(1000);
                    }, 3000);
                }
            });
        }
    });

    $('#modalReservation').on('click', function (e) {
        var tourId =  $(this).data('tourid');
        $('#tourId').val(tourId);
    });

    // Add new reservation
    $('#saveReservationBtn').on('click', function (e) {
        debugger
        e.preventDefault();
        var reservationData = {
            Client: $('#ReservationClient').val(),
            ReservationDate: $('#ReservationDate').val(),
            TourId: parseInt($('#tourId').val(),10)
        };

        $.ajax({
            type: 'POST',
            url: $(this).data('url'),
            data: JSON.stringify(reservationData),
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                $('#addReservationModal').hide();
                $('#alertok').text("Reserva Agregada.");
                $('#alertok').fadeIn(1000);        
                setTimeout(function () {
                    $('#alertok').fadeOut(1000);
                }, 3000);
            },
            error: function (error) {
                $('#alerterror').text("Error al agregar.");
                $('#alerterror').fadeIn(1000);
                setTimeout(function () {
                    $('#alerterror').fadeOut(1000);
                }, 3000);
            }
        });
    });

    // Add new tour
    $('#saveTourBtn').on('click', function (e) {
        
        e.preventDefault();
        var tourData = {
            Name: $('#tourName').val(),
            Destination: $('#tourDestination').val(),
            StartDate: $('#tourStartDate').val(),
            EndDate: $('#tourEndDate').val(),
            Price: parseFloat($('#tourPrice').val())
        };

        $.ajax({
            type: 'POST',
            url: $(this).data('url'),
            data: JSON.stringify(tourData),
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                $('#addTourModal').modal('hide');
                $('#alertok').text("Tour Agregado.");
                $('#alertok').fadeIn(1000);
                setTimeout(function () {
                    $('#alertok').fadeOut(1000);
                }, 3000);
            },
            error: function (error) {
                $('#alerterror').text("Error al agregar.");
                $('#alerterror').fadeIn(1000);
                setTimeout(function () {
                    $('#alerterror').fadeOut(1000);
                }, 3000);
            }
        });
    });


    $('#seeReservations').on('click', function () {
        $.ajax({
            url: $(this).data('url'),
            data: { id: parseInt($(this).data('tourid')) },
            type: 'GET',
            success: function (result) {
                $('#container').html(result);
            },
            error: function () {
                $('#alerterror').text("Error.");
                $('#alerterror').fadeIn(1000);
                setTimeout(function () {
                    $('#alerterror').fadeOut(1000);
                }, 3000);
            }
        });
    });
